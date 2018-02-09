using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : MonoBehaviour {

    [SerializeField]
    enum ZombieStates
    {
        Idle = 0,
        Scream,
        Chase,
        Bite,
        Suffer,
        Die,
    }

    private ZombieStates zombieStates;
    private ZombieStates savedState;

    private float walkSpeed;
    //Player 스크립트에서 불러 올 값이라 public으로 설정
    public float zombieHP;

    private GameObject player;
    private float changeTime;
    private bool animIsDone;

    Animator animator;
    //Animation animation;

    void Awake()
    {
        zombieStates = ZombieStates.Idle;
        walkSpeed = 0.05f;
        zombieHP = 100;
        changeTime = 0.0f;
        animIsDone = false;

        player = GameObject.Find("SWAT");

        animator = GetComponent<Animator>();
    }

	void Start () {

    }
	
	void Update () {
        CheckStates();
        print("타이머" + changeTime);
        print("현재 상태" + " " + zombieStates);
        print("저장된 상태" +" "+savedState);
        BehaveActions();
    }

    void RememberState()
    {
        savedState = zombieStates;
    }

    void Return2PreviousState()
    {
        zombieStates = savedState;
    }

    void CheckStates()
    {
        if (Vector3.Distance(player.transform.position, this.transform.position) < 6 && Vector3.Distance(player.transform.position, this.transform.position) > 0.7f && zombieHP > 0)
        {
            changeTime += Time.deltaTime;
            zombieStates = ZombieStates.Scream;

            if (changeTime > 2.3f)
            {
                zombieStates = ZombieStates.Chase;
                changeTime = 0.0f;
            }
        }
        else if (Vector3.Distance(player.transform.position, this.transform.position) < 0.7f)
            zombieStates = ZombieStates.Bite;
        else if (zombieHP <= 0)
            zombieStates = ZombieStates.Die;
        else if (zombieHP != 100 && zombieHP % 20 == 0 && animIsDone == false)
        {
            zombieStates = ZombieStates.Suffer;
            //changeTime += Time.deltaTime;
            //if(changeTime >  )
        }
    }

    void BehaveActions()
    {
        Vector3 direction = player.transform.position - this.transform.position;
        direction.y = 0;

        switch (zombieStates)
        {
            case ZombieStates.Idle:
                RememberState();
                animator.SetBool("isScreaming", false);
                animator.SetBool("isWalking", false);
                animator.SetBool("isBiting", false);
                break;
            case ZombieStates.Scream:
                RememberState();
                direction = player.transform.position - this.transform.position;
                direction.y = 0;
                this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), 0.1f);

                animator.SetBool("isIdle", false);
                animator.SetBool("isScreaming", true);
                break;
            case ZombieStates.Chase:
                RememberState();
                animator.SetBool("isWalking", true);
                animator.SetBool("isBiting", false);
                animator.SetBool("isIdle", false);
                animator.SetBool("isScreaming", false);
                //쫒아오면서 방향도 주시
                this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), 0.1f);
                this.transform.position = Vector3.Slerp(this.transform.position, player.transform.position, walkSpeed * Time.deltaTime);
                break;
            case ZombieStates.Bite:
                RememberState();
                animator.SetBool("isScreaming", false);
                animator.SetBool("isBiting", true);
                animator.SetBool("isWalking", false);
                break;
            case ZombieStates.Suffer:
                animator.SetBool("isIdle", false);
                animator.SetBool("isScreaming", false);
                animator.SetBool("isWalking", false);
                animator.SetBool("isBiting", false);
                animator.SetBool("isSuffering", true);
                animator.SetBool("isDead", false);
                animator.SetBool("isIdle", false);
                Return2PreviousState();
                break;
            case ZombieStates.Die:
                this.transform.position = Vector3.Slerp(this.transform.position, this.transform.position, walkSpeed * Time.deltaTime);

                animator.SetBool("isIdle", false);
                animator.SetBool("isScreaming", false);
                animator.SetBool("isWalking", false);
                animator.SetBool("isBiting", false);
                animator.SetBool("isSuffering", false);
                animator.SetBool("isDead", true);
                animator.SetBool("isIdle", false);
                break;
       }
        
    }

    //IEnumerator CheckZombieStates()
    //{
    //    while(!isDead)
    //    {
    //        yield return new WaitForSeconds(0.2f);

    //        if (Vector3.Distance(player.transform.position, this.transform.position) < 6 && Vector3.Distance(player.transform.position, this.transform.position) > 0.7f)
    //            zombieStates = ZombieStates.Chase;
    //        else if (Vector3.Distance(player.transform.position, this.transform.position) < 0.7f)
    //            zombieStates = ZombieStates.Bite;
    //        else
    //            zombieStates = ZombieStates.Idle;

    //        if (zombieHP <= 0)
    //            zombieStates = ZombieStates.Die;

    //        //if(zombieStates == ZombieStates.Die)
    //        //{
    //        //    changeTime += Time.deltaTime;

    //        //    if(changeTime > 1.0f)
    //        //    {
    //        //        zombieStates = ZombieStates.Lie;
    //        //    }
    //        //}
    //    }
    //}

    //IEnumerator BehaveZombieAction()
    //{
    //    while (!isDead)
    //    { 
    //        switch(zombieStates)
    //        {
    //            case ZombieStates.Idle:
    //                //가만히 있을 시 자리 그대로
    //                this.transform.position = this.transform.position;

    //                animator.SetBool("isScreaming", false);
    //                animator.SetBool("isWalking", false);
    //                animator.SetBool("isBiting", false);
    //                break;

    //            //case ZombieStates.Scream:
    //            //    //플레이어 방향으로 회전
    //            //    Vector3 direction = player.transform.position - this.transform.position;
    //            //    direction.y = 0;
    //            //    this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), 0.1f);
    //            //    //플레이어를 향해 소리지름
    //            //    animator.SetBool("isIdle", false);
    //            //    animator.SetBool("isWalking", false);
    //            //    animator.SetBool("isBiting", false);
    //            //    break;

    //            case ZombieStates.Chase:
    //                animator.SetBool("isIdle", false);
    //                animator.SetBool("isScreaming", false);
    //                animator.SetBool("isWalking", true);

    //                Vector3 direction = player.transform.position - this.transform.position;
    //                direction.y = 0;
    //                this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), 0.1f);

    //                this.transform.position = Vector3.Slerp(this.transform.position, player.transform.position, walkSpeed * Time.deltaTime);
    //                break;

    //            case ZombieStates.Bite:
    //                this.transform.position = this.transform.position;

    //                animator.SetBool("isScreaming", false);
    //                animator.SetBool("isBiting", true);
    //                animator.SetBool("isWalking", false);
    //                break;

    //            case ZombieStates.Die:
    //                this.transform.position = Vector3.Slerp(this.transform.position, this.transform.position, walkSpeed * Time.deltaTime);
    //                animator.SetBool("isDead", true);
    //                animator.SetBool("isIdle", false);
    //                animator.SetBool("isScreaming", false);
    //                animator.SetBool("isWalking", false);
    //                animator.SetBool("isBiting", false);
    //                animator.SetBool("isSuffering", false);
    //                break;

    //            //case ZombieStates.Lie:
    //            //    animator.StopPlayback();
    //            //    break;
    //        }

    //        yield return null;
    //    }

    //}
}
