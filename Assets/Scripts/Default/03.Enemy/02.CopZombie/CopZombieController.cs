using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using debug = UnityEngine.Debug;


public class CopZombieController : MonoBehaviour {

    [SerializeField]
    enum ZombieState
    {
        Idle = 0,
        Scream,
        Chase,
        Bite,
        Suffer,
        Die,
    }

    private ZombieState zombieState;
    private ZombieState savedState;

    private float walkSpeed;

    //Player 스크립트에서 불러 올 값이라 public으로 설정
    public float zombieHP;

    private GameObject player;
    Animator animator;

    private Stopwatch sw = new Stopwatch();

    void Awake()
    {
        walkSpeed = 0.1f;
        zombieHP = 100;

        player = GameObject.Find("SWAT");

        animator = GetComponent<Animator>();
    }

    void Start()
    {
        zombieState = ZombieState.Idle;
    }

    void Update() {
        sw.Start();
        //debug.Log("sw : " + sw.ElapsedMilliseconds.ToString() + "ms");
        //if (sw.ElapsedMilliseconds > 3000f)

        //print("타이머" + changeTime);
        //print("현재 상태" + " " + zombieStates);
        //print("저장된 상태" + " " + savedState);
        //print("경찰 좀비" + zombieHP);
        BehaveZombieFSM();
    }

    void RememberState()
    {
        savedState = zombieState;
    }

    void Return2PreviousState()
    {
        zombieState = savedState;
    }

    //void CheckStates()
    //{
    //    if (Vector3.Distance(player.transform.position, this.transform.position) < 6 && Vector3.Distance(player.transform.position, this.transform.position) > 0.7f && zombieHP > 0)
    //    {
    //        changeTime += Time.deltaTime;
    //        zombieState = ZombieState.Scream;

    //        if (changeTime > 2.3f)
    //        {
    //            zombieState = ZombieState.Chase;
    //            changeTime = 0.0f;
    //        }
    //    }
    //    else if (Vector3.Distance(player.transform.position, this.transform.position) < 0.7f)
    //        zombieState = ZombieState.Bite;
    //    else if (zombieHP <= 0)
    //        zombieState = ZombieState.Die;
    //    else if (zombieHP != 100 && zombieHP % 20 == 0 && animIsDone == false)
    //        zombieState = ZombieState.Suffer;
        
    //}

    //void BehaveActions()
    //{
    //    Vector3 direction = player.transform.position - this.transform.position;
    //    direction.y = 0;

    //    switch (zombieState)
    //    {
    //        case ZombieState.Idle:
    //            RememberState();
    //            animator.SetBool("isScreaming", false);
    //            animator.SetBool("isWalking", false);
    //            animator.SetBool("isBiting", false);
    //            break;
    //        case ZombieState.Scream:
    //            RememberState();
    //            direction = player.transform.position - this.transform.position;
    //            direction.y = 0;
    //            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), 0.1f);

    //            animator.SetBool("isIdle", false);
    //            animator.SetBool("isScreaming", true);
    //            break;
    //        case ZombieState.Chase:
    //            RememberState();
    //            animator.SetBool("isWalking", true);
    //            animator.SetBool("isBiting", false);
    //            animator.SetBool("isIdle", false);
    //            animator.SetBool("isScreaming", false);
    //            //쫒아오면서 방향도 주시
    //            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), 0.1f);
    //            this.transform.position = Vector3.Slerp(this.transform.position, player.transform.position, walkSpeed * Time.deltaTime);
    //            break;
    //        case ZombieState.Bite:
    //            RememberState();
    //            animator.SetBool("isScreaming", false);
    //            animator.SetBool("isBiting", true);
    //            animator.SetBool("isWalking", false);
    //            break;
    //        case ZombieState.Suffer:
    //            animator.SetBool("isIdle", false);
    //            animator.SetBool("isScreaming", false);
    //            animator.SetBool("isWalking", false);
    //            animator.SetBool("isBiting", false);
    //            animator.SetBool("isSuffering", true);
    //            animator.SetBool("isDead", false);
    //            animator.SetBool("isIdle", false);
    //            Return2PreviousState();
    //            break;
    //        case ZombieState.Die:
    //            this.transform.position = Vector3.Slerp(this.transform.position, this.transform.position, walkSpeed * Time.deltaTime);

    //            animator.SetBool("isIdle", false);
    //            animator.SetBool("isScreaming", false);
    //            animator.SetBool("isWalking", false);
    //            animator.SetBool("isBiting", false);
    //            animator.SetBool("isSuffering", false);
    //            animator.SetBool("isDead", true);
    //            animator.SetBool("isIdle", false);
    //            break;
    //    }

    //}

    void BehaveZombieFSM()
    {
        Vector3 direction = player.transform.position - this.transform.position;
        direction.y = 0;

        switch (zombieState)
        {
            case ZombieState.Idle:
                sw.Reset();
                animator.SetBool("isIdle", true);
                animator.SetBool("isScreaming", false);
                animator.SetBool("isWalking", false);
                animator.SetBool("isBiting", false);
                animator.SetBool("isSuffering", false);
                animator.SetBool("isDead", false);
                animator.SetBool("isIdle", false);

                if (Vector3.Distance(player.transform.position, this.transform.position) < 6)
                    zombieState = ZombieState.Scream;

                if (zombieHP != 100)
                    zombieState = ZombieState.Scream;

                if (zombieHP <= 0)
                    zombieState = ZombieState.Die;
                break;
            case ZombieState.Scream:
                direction = player.transform.position - this.transform.position;
                this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.LookRotation(direction), 0.1f);

                animator.SetBool("isIdle", false);
                animator.SetBool("isScreaming", true);
                animator.SetBool("isWalking", false);
                animator.SetBool("isBiting", false);
                animator.SetBool("isSuffering", false);
                animator.SetBool("isDead", false);

                sw.Start();

                if (sw.ElapsedMilliseconds > 3300)
                    zombieState = ZombieState.Chase;

                if (zombieHP <= 0)
                    zombieState = ZombieState.Die;

                break;
            case ZombieState.Chase:
                sw.Reset();
                animator.SetBool("isIdle", false);
                animator.SetBool("isScreaming", false);
                animator.SetBool("isWalking", true);
                animator.SetBool("isBiting", false);
                animator.SetBool("isSuffering", false);
                animator.SetBool("isDead", false);

                direction = player.transform.position - this.transform.position;
                this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), 0.1f);
                this.transform.position = Vector3.Lerp(this.transform.position, player.transform.position, walkSpeed * Time.deltaTime);

                if(Vector3.Distance(player.transform.position, this.transform.position) < 0.7f)
                    zombieState = ZombieState.Bite;

                if (zombieHP <= 0)
                    zombieState = ZombieState.Die;

                break;
            case ZombieState.Bite:
                this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), 0.1f);
                
                animator.SetBool("isIdle", false);
                animator.SetBool("isScreaming", false);
                animator.SetBool("isWalking", false);
                animator.SetBool("isBiting", true);
                animator.SetBool("isSuffering", false);
                animator.SetBool("isDead", false);

                if (Vector3.Distance(player.transform.position, this.transform.position) >= 0.7f)
                {
                    sw.Start();

                    if (sw.ElapsedMilliseconds > 3600)
                        zombieState = ZombieState.Chase;
                }

                if (zombieHP <= 0)
                    zombieState = ZombieState.Die;

                break;
            case ZombieState.Die:
                this.transform.position = Vector3.Lerp(this.transform.position, player.transform.position, 0.0f * Time.deltaTime);

                animator.SetBool("isIdle", false);
                animator.SetBool("isScreaming", false);
                animator.SetBool("isWalking", false);
                animator.SetBool("isBiting", false);
                animator.SetBool("isSuffering", false);
                animator.SetBool("isDead", true);

                break;
        }
    }

    public void IsBeingDamaged(float weaponDmg)
    {
        zombieHP -= weaponDmg;
    }

}
