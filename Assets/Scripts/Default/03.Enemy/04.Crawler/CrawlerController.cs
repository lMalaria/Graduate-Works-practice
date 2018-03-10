using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using debug = UnityEngine.Debug;

public class CrawlerController : MonoBehaviour {

    enum ZombieState
    {
        Wander = 0,
        Notice,
        Chase,
        Jump,
        Die
    }

    ZombieState zombieState;

    private Stopwatch sw = new Stopwatch();

    private GameObject player;

    Animator animator;

    private float wanderSpeed;

    private float rotationSpeed;

    private int rotationLeftOrRight;

    private float zombieHP;

    private Vector3 patrolScope;

    void Awake()
    {
        player = GameObject.Find("SWAT");
        animator = GetComponent<Animator>();

        zombieHP = 100;

    }

	void Start ()
    {
        sw.Reset();
        zombieState = ZombieState.Wander;
        InvokeRepeating("CheckPatrolDirection", 8, 9);
    }
	
	void Update ()
    {
        CrawlerFSM();
        print(rotationLeftOrRight);
    }

    void CrawlerFSM()
    {
        Vector3 direction = player.transform.position - this.transform.position;
        direction.y = 0;

        if (zombieHP <= 0)
            zombieState = ZombieState.Die;

        switch(zombieState)
        {
            case ZombieState.Wander:
                animator.SetBool("isPatrolling", true);
                animator.SetBool("isRunning", false);
                animator.SetBool("isJumping", false);
                animator.SetBool("isDead", false);

                this.transform.position += this.transform.forward * 0.007f;
                sw.Start();
                //if (sw.ElapsedMilliseconds > 2000)
                //{

                //    this.transform.position += this.transform.forward * 0.007f;

                //    //    int rotationWait = Random.Range(5, 8);
                //    //transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.Euler(this.transform.rotation.x, -this.transform.rotation.y, this.transform.rotation.z), Time.deltaTime*10);
                //}
                if(sw.ElapsedMilliseconds > 8000)
                {
                    if (rotationLeftOrRight == 1)
                        transform.Rotate(0, 90 * Time.deltaTime, 0);
                    
                    else if (rotationLeftOrRight == 2)
                        transform.Rotate(0, -90 * Time.deltaTime, 0);
                }

                if(sw.ElapsedMilliseconds > 9000)
                    sw.Reset();
                
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

    void CheckPatrolDirection()
    {
        rotationLeftOrRight = Random.Range(1, 3);
    }
}
