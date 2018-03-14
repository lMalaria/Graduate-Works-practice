using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using debug = UnityEngine.Debug;

public class TutorialBossController : MonoBehaviour {
    
    public enum BossState
    {
        Idle = 0,
        Die
    }

    BossState bossState;

    private GameObject player;

    private Stopwatch sw = new Stopwatch();

    Animator animator;

    private float BossHP;

    void Awake()
    {
        player = GameObject.Find("SWAT");
        animator = GetComponent<Animator>();

        BossHP = 100;
    }

	void Start ()
    {
        sw.Reset();
        bossState = BossState.Idle;

	}
	
	void Update ()
    {
        BossFSM();
	}

    void BossFSM()
    {
        Vector3 direction = player.transform.position - this.transform.position;
        direction.y = 0;

        switch (bossState)
        {
            case BossState.Idle:

                break;
            case BossState.Die:

                break;
        }
    }
}
