using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class BombZombieController : MonoBehaviour {

    [SerializeField]
    enum ZombieState
    {
        Idle = 0,
        Notice,
        Chase,
        Bang,
        Die,
    }

    private ZombieState zombieState;

    private float runSpeed;

    //Player 스크립트에서 불러 올 값이라 public으로 설정
    public float zombieHP;

    private GameObject player;

    Animator animator;

    private Stopwatch sw = new Stopwatch();

    SWATPlayerController playerController;

    void Awake()
    {
        zombieState = ZombieState.Idle;
        runSpeed = 0.5f;
        zombieHP = 100;

        player = GameObject.Find("SWAT");

        animator = GetComponent<Animator>();
    }

    void Start ()
    {
		
	}

	void Update ()
    {
        print("자살맨 " + zombieHP);
        BehaveZombieFSM();
	}

    void BehaveZombieFSM()
    {
        Vector3 direction = player.transform.position - this.transform.position;
        direction.y = 0;

        if (zombieHP <= 0)
            zombieState = ZombieState.Die;

        switch(zombieState)
        {
            case ZombieState.Idle:
                animator.SetBool("isIdle", true);
                animator.SetBool("isNoticing", false);
                animator.SetBool("isRunning", false);

                if (Vector3.Distance(player.transform.position, this.transform.position) < 6)
                    zombieState = ZombieState.Notice;

                break;

            case ZombieState.Notice:
                direction = player.transform.position - this.transform.position;
                this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), 0.1f);

                animator.SetBool("isIdle", false);
                animator.SetBool("isNoticing", true);
                animator.SetBool("isRunning", false);

                sw.Start();

                if (sw.ElapsedMilliseconds > 2000)
                    zombieState = ZombieState.Chase;

                break;

            case ZombieState.Chase:
                animator.SetBool("isIdle", false);
                animator.SetBool("isNoticing", false);
                animator.SetBool("isRunning", true);

                direction = player.transform.position - this.transform.position;
                this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), 0.1f);
                this.transform.position = Vector3.Slerp(this.transform.position, player.transform.position, runSpeed * Time.deltaTime);

                if (Vector3.Distance(player.transform.position, this.transform.position) < 0.4f)
                    zombieState = ZombieState.Bang;

                break;

            case ZombieState.Bang:
                sw.Reset();
                if (Vector3.Distance(player.transform.position, this.transform.position) >= 0.5f)
                    zombieState = ZombieState.Chase;

                if (Vector3.Distance(player.transform.position, this.transform.position) < 0.5f)
                {
                    playerController = player.GetComponent<SWATPlayerController>();
                    playerController.IsHurt(100f);
                    Destroy(this.gameObject);
                }

                

                break;

            case ZombieState.Die:
                Destroy(this.gameObject);
                break;
        }
    }

    public void IsBeingDamged(float weaponDmg)
    {
        zombieHP -= weaponDmg;
    }
}
