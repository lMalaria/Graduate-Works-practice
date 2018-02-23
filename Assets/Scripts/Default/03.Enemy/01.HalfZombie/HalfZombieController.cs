using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using debug = UnityEngine.Debug;

public class HalfZombieController : MonoBehaviour {

    [SerializeField]
    enum ZombieState
    {
        Idle = 0,
        Notice,
        Chase,
        BasicAttack,
        Slash,
        Die
    }

    ZombieState zombieState;

    private GameObject player;

    private float zombieHP;

    private Stopwatch sw = new Stopwatch();

    Animator animator;

    private float slashCount;

    void Awake()
    {
        player = GameObject.Find("SWAT");
        animator = GetComponent<Animator>();
        zombieHP = 100;
        slashCount = 0;
    }

    void Start ()
    {
        this.transform.eulerAngles = new Vector3(0, -90, 0);
        zombieState = ZombieState.Idle;
	}
	
	void Update ()
    {
        HalfZombieFSM();
        print(slashCount);
	}

    void HalfZombieFSM()
    {
        Vector3 direction = player.transform.position - this.transform.position;
        direction.y = 0;

        if (zombieHP == 0)
            zombieState = ZombieState.Die;

        switch(zombieState)
        {
            case ZombieState.Idle:
                animator.SetBool("isIdle", true);
                animator.SetBool("isTurning", false);
                animator.SetBool("isRunning", false);
                animator.SetBool("isAttacking", false);
                animator.SetBool("isSlashing", false);
                animator.SetBool("isDead", false);

                if (Vector3.Distance(player.transform.position, this.transform.position) < 4)
                    zombieState = ZombieState.Notice;

                if (zombieHP != 100)
                    zombieState = ZombieState.Notice;

                break;
            case ZombieState.Notice:
                //direction = player.transform.position - this.transform.position;
                //this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.LookRotation(direction), 0.1f);
                animator.SetBool("isIdle", false);
                animator.SetBool("isTurning", true);
                animator.SetBool("isRunning", false);
                animator.SetBool("isAttacking", false);
                animator.SetBool("isSlashing", false);
                animator.SetBool("isDead", false);

                sw.Start();

                if (sw.ElapsedMilliseconds > 3000)
                    zombieState = ZombieState.Chase;

                break;
            case ZombieState.Chase:
                sw.Reset();
                animator.SetBool("isIdle", false);
                animator.SetBool("isTurning", false);
                animator.SetBool("isRunning", true);
                animator.SetBool("isAttacking", false);
                animator.SetBool("isSlashing", false);
                animator.SetBool("isDead", false);

                direction = player.transform.position - this.transform.position;
                this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), 0.1f);

                this.transform.position = Vector3.Lerp(this.transform.position, player.transform.position, Time.deltaTime);

                if (Vector3.Distance(player.transform.position, this.transform.position) < 0.4f && slashCount % 3 != 1)
                    zombieState = ZombieState.BasicAttack;
                      
                if (Vector3.Distance(player.transform.position, this.transform.position) < 0.4f && slashCount % 3 == 1)
                    zombieState = ZombieState.Slash;

                break;
            case ZombieState.BasicAttack:
                animator.SetBool("isIdle", false);
                animator.SetBool("isTurning", false);
                animator.SetBool("isRunning", false);
                animator.SetBool("isAttacking", true);
                animator.SetBool("isSlashing", false);
                animator.SetBool("isDead", false);

                direction = player.transform.position - this.transform.position;
                this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), 0.1f);

                if (Vector3.Distance(player.transform.position, this.transform.position) >= 0.4f)
                {
                    sw.Start();

                    if (sw.ElapsedMilliseconds > 2000)
                        zombieState = ZombieState.Chase;
                }

                break;

            case ZombieState.Slash:
                animator.SetBool("isIdle", false);
                animator.SetBool("isTurning", false);
                animator.SetBool("isRunning", false);
                animator.SetBool("isAttacking", false);
                animator.SetBool("isSlashing", true);
                animator.SetBool("isDead", false);

                direction = player.transform.position - this.transform.position;
                this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), 0.1f);

                if (Vector3.Distance(player.transform.position, this.transform.position) >= 0.3f)
                {
                    sw.Start();

                    if (sw.ElapsedMilliseconds > 2000)
                        zombieState = ZombieState.Chase;
                }

                break;
            case ZombieState.Die:
                animator.SetBool("isIdle", false);
                animator.SetBool("isTurning", false);
                animator.SetBool("isRunning", false);
                animator.SetBool("isAttacking", false);
                animator.SetBool("isSlashing", false);
                animator.SetBool("isDead", true);

                break;
        }
    }

    public void IsBeingDamaged(float weaponDmg)
    {
        zombieHP -= weaponDmg;
    }

}
