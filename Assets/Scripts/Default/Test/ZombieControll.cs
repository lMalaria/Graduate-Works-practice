using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieControll : MonoBehaviour {

    [SerializeField]
    enum ZombieStates
    {
        Idle = 0,
        Scream,
        Chase,
        Bite,
        Suffer,
        Die,
        Lie
    }

    private ZombieStates zombieStates;

    private float walkSpeed;
    //Player 스크립트에서 불러 올 값이라 public으로 설정
    public float zombieHP;
    private bool isDead;

    private GameObject player;
    private float disappearTime;
    private float changeTime;

    Animator animator;
    //Animation animation;

    void Awake()
    {
        zombieStates = ZombieStates.Idle;
        walkSpeed = 0.05f;
        zombieHP = 100;

        player = GameObject.Find("SWAT");

        animator = GetComponent<Animator>();
        disappearTime = 0.0f;
        changeTime = 0.0f;
    }

	void Start () {
        StartCoroutine(this.CheckZombieStates());
        StartCoroutine(this.BehaveZombieAction());
        //animation = GetComponent<Animation>();
    }
	
	void Update () {

        //if (Vector3.Distance(player.transform.position, this.transform.position) < 6)
        //{
        //    Vector3 direction = player.transform.position - this.transform.position;
        //    direction.y = 0;

        //    this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), 0.1f);

        //    animator.SetBool("isIdle", false);

        //    if (direction.magnitude < 5)
        //    {
        //        animator.SetBool("isWalking", true);
        //        animator.SetBool("isBiting", false);
        //        this.transform.position = Vector3.Slerp(this.transform.position, player.transform.position + new Vector3(0, 0, 0.2f), walkSpeed * Time.deltaTime);

        //        if(zombieHP != 100 && zombieHP % 20 == 0)
        //        {
        //            timer += Time.deltaTime;
        //            walkSpeed = 0.0f;
        //            animator.SetBool("isSuffering",true);
        //            animator.SetBool("isWalking", false);

        //            if(timer > 3.0f)
        //            {
        //                walkSpeed = 0.2f;
        //                animator.SetBool("isWalking", true);
        //                animator.SetBool("isSuffering", false);
        //            }
        //        }
        //    }

        //    if (direction.magnitude < 0.7f)
        //    {
        //        walkSpeed = 0.0f;
        //        //this.transform.position = Vector3.Slerp(this.transform.position, this.transform.position, 0.2f * Time.deltaTime);
        //        animator.SetBool("isBiting", true);
        //        animator.SetBool("isWalking", false);
        //    }
        //}

        //if (zombieHP != 100 && zombieHP % 20 == 0)
        //{
        //    //timer += Time.deltaTime;
        //    animator.SetBool("isSuffering", true);
        //    animator.SetBool("isBiting", false);
        //    animator.SetBool("isWalking", false);
        //    animator.SetBool("isIdle", false);
        //    animator.SetBool("isScreaming", false);
        //}

        //    if(timer > 5.0f)
        //    {
        //        timer = 0.0f;
        //        //animator.SetBool("isWalking");
        //    }
        //}

        if (zombieHP == 80)
        {
            disappearTime += Time.deltaTime;

            walkSpeed = 0.0f;
            animator.SetBool("isIdle", false);
            animator.SetBool("isScreaming", false);
            animator.SetBool("isWalking", false);
            animator.SetBool("isBiting", false);
            animator.SetBool("isSuffering", true);


            if (disappearTime > 3.0f)
            {
                animator.SetBool("isSuffering", false);
                walkSpeed = 0.05f;
                this.transform.position = Vector3.Slerp(this.transform.position, player.transform.position, walkSpeed * Time.deltaTime);
            }
        }
    }

    IEnumerator CheckZombieStates()
    {
        while(!isDead)
        {
            yield return new WaitForSeconds(0.2f);

            if (Vector3.Distance(player.transform.position, this.transform.position) < 6 && Vector3.Distance(player.transform.position, this.transform.position) > 0.7f)
                zombieStates = ZombieStates.Chase;
            else if (Vector3.Distance(player.transform.position, this.transform.position) < 0.7f)
                zombieStates = ZombieStates.Bite;
            else
                zombieStates = ZombieStates.Idle;

            if (zombieHP <= 0)
                zombieStates = ZombieStates.Die;

            //if(zombieStates == ZombieStates.Die)
            //{
            //    changeTime += Time.deltaTime;

            //    if(changeTime > 1.0f)
            //    {
            //        zombieStates = ZombieStates.Lie;
            //    }
            //}
        }
    }

    IEnumerator BehaveZombieAction()
    {
        while (!isDead)
        { 
            switch(zombieStates)
            {
                case ZombieStates.Idle:
                    //가만히 있을 시 자리 그대로
                    this.transform.position = this.transform.position;

                    animator.SetBool("isScreaming", false);
                    animator.SetBool("isWalking", false);
                    animator.SetBool("isBiting", false);
                    break;

                //case ZombieStates.Scream:
                //    //플레이어 방향으로 회전
                //    Vector3 direction = player.transform.position - this.transform.position;
                //    direction.y = 0;
                //    this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), 0.1f);
                //    //플레이어를 향해 소리지름
                //    animator.SetBool("isIdle", false);
                //    animator.SetBool("isWalking", false);
                //    animator.SetBool("isBiting", false);
                //    break;

                case ZombieStates.Chase:
                    animator.SetBool("isIdle", false);
                    animator.SetBool("isScreaming", false);
                    animator.SetBool("isWalking", true);

                    Vector3 direction = player.transform.position - this.transform.position;
                    direction.y = 0;
                    this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), 0.1f);

                    this.transform.position = Vector3.Slerp(this.transform.position, player.transform.position, walkSpeed * Time.deltaTime);
                    break;

                case ZombieStates.Bite:
                    this.transform.position = this.transform.position;

                    animator.SetBool("isScreaming", false);
                    animator.SetBool("isBiting", true);
                    animator.SetBool("isWalking", false);
                    break;

                case ZombieStates.Die:
                    this.transform.position = Vector3.Slerp(this.transform.position, this.transform.position, walkSpeed * Time.deltaTime);
                    animator.SetBool("isDead", true);
                    animator.SetBool("isIdle", false);
                    animator.SetBool("isScreaming", false);
                    animator.SetBool("isWalking", false);
                    animator.SetBool("isBiting", false);
                    animator.SetBool("isSuffering", false);
                    break;

                //case ZombieStates.Lie:
                //    animator.StopPlayback();
                //    break;
            }

            yield return null;
        }

    }
}
