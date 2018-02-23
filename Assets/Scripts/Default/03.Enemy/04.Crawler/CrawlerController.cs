using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using debug = UnityEngine.Debug;

public class CrawlerController : MonoBehaviour {

    enum ZombieState
    {
        HangAround = 0,
        Notice,
        Chase,
        Jump,
        Die
    }

    ZombieState zombieState;

    private Stopwatch sw = new Stopwatch();

    private GameObject player;

    Animator animator;

    private float zombieHP;

    private float patrolScopeXAxis;

    private float patrolScopeYAxis;

    private float patrolScopeZAxis;

    private Vector3 patrolScope;

    private bool isPatrolling;

    void Awake()
    {
        player = GameObject.Find("SWAT");
        animator = GetComponent<Animator>();
        zombieHP = 100;
        patrolScopeYAxis = -0.57f;
        isPatrolling = true;
    }

	void Start ()
    {
        zombieState = ZombieState.HangAround;
    }
	
	void Update ()
    {
        CrawlerFSM();
	}

    void CrawlerFSM()
    {
        Vector3 direction = player.transform.position - this.transform.position;
        direction.y = 0;

        if (zombieHP <= 0)
            zombieState = ZombieState.Die;

        switch(zombieState)
        {
            case ZombieState.HangAround:
                animator.SetBool("isPatrolling", true);
                animator.SetBool("isRunning", false);
                animator.SetBool("isJumping", false);
                animator.SetBool("isDead", false);

                if (Vector3.Distance(player.transform.position, this.transform.position) < 7)
                    zombieState = ZombieState.Notice;

                break;
            case ZombieState.Notice:
                animator.SetBool("isPatrolling", false);
                animator.SetBool("isRunning", false);
                animator.SetBool("isJumping", false);
                animator.SetBool("isDead", false);

                direction = player.transform.position - this.transform.position;
                this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), 0.1f);

                sw.Start();
                if (sw.ElapsedMilliseconds > 2000)
                    zombieState = ZombieState.Chase;

                break;

            case ZombieState.Chase:
                sw.Reset();
                animator.SetBool("isPatrolling", false);
                animator.SetBool("isRunning", true);
                animator.SetBool("isJumping", false);
                animator.SetBool("isDead", false);

                direction = player.transform.position - this.transform.position;
                this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), 0.1f);
                this.transform.position = Vector3.Lerp(this.transform.position, player.transform.position, Time.deltaTime * 0.5f);

                if (Vector3.Distance(player.transform.position, this.transform.position) < 2)
                    zombieState = ZombieState.Jump;

                break;

            case ZombieState.Jump:
                sw.Start();
                direction = player.transform.position - this.transform.position;
                this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), 0.1f);
                this.transform.position = Vector3.Lerp(this.transform.position, player.transform.position, Time.deltaTime * 0.5f);

                animator.SetBool("isPatrolling", false);
                animator.SetBool("isRunning", false);
                animator.SetBool("isJumping", true);
                animator.SetBool("isDead", false);

                if (sw.ElapsedMilliseconds > 1500)
                    zombieState = ZombieState.Chase;

                break;

            case ZombieState.Die:
                animator.SetBool("isPatrolling", false);
                animator.SetBool("isRunning", false);
                animator.SetBool("isJumping", true);
                animator.SetBool("isDead", true);

                Destroy(this.gameObject, 2.0f);
                break;
        }
    }

    public void IsBeingDamaged(float weaponDmg)
    {
        zombieHP -= weaponDmg;
    }
}
