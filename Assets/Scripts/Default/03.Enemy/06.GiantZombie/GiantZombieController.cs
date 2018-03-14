using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using debug = UnityEngine.Debug;

public class GiantZombieController : MonoBehaviour {

    public enum ZombieState
    {
        idle = 0,
        Wander,
        Notice,
        Chase,
        Confront,
        SwingVertically,
        SwingSpirally,
        Throw,
        Die
    }

    ZombieState zombieState;

    private Stopwatch sw = new Stopwatch();

    private GameObject player;

    Animator animator;

    private float zombieHP;

    void Awake()
    {
        player = GameObject.Find("SWAT");
        animator = GetComponent<Animator>();

        zombieHP = 100;
    }

	void Start ()
    {
        sw.Reset();
        zombieState = ZombieState.idle;
	}

	void Update ()
    {
        GiantZombieFSM();
	}

    void GiantZombieFSM()
    {
        Vector3 direction = player.transform.position - this.transform.position;
        direction.y = 0;

        switch (zombieState)
        {
            case ZombieState.idle:

                break;

            case ZombieState.Wander:

                break;

            case ZombieState.Notice:

                break;

            case ZombieState.Chase:

                break;

            case ZombieState.Confront:

                break;

            case ZombieState.SwingVertically:

                break;

            case ZombieState.SwingSpirally:

                break;

            case ZombieState.Throw:

                break;

            case ZombieState.Die:

                break;
        }
    }
}
