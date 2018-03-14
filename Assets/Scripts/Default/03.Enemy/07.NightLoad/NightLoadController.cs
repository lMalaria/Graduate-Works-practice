using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using debug = UnityEngine.Debug;

public class NightLoadController : MonoBehaviour {

    public enum NightLoadState
    {
        Idle = 0,
        Patrol,
        Notice,
        Chase,
        JumpingAttack,
        Punch,
        Kick,
        Bite,
        KillInstantly,
        AbsorbCompanies,
        Metamorphosis,
        Die
    }

    NightLoadState nightLoadState;

    private Stopwatch sw = new Stopwatch();

    Animator animator;

    private GameObject player;

    private float nightLoadHP;

    void Awake()
    {
        player = GameObject.Find("SWAT");
        animator = GetComponent<Animator>();

        nightLoadHP = 100;
    }

	void Start ()
    {
        sw.Reset();
        nightLoadState = NightLoadState.Patrol;
	}
	
	void Update ()
    {
        NightLoadFSM();	
	}

    void NightLoadFSM()
    {
        Vector3 direction = player.transform.position - this.transform.position;
        direction.y = 0;

        switch (nightLoadState)
        {
            case NightLoadState.Idle:

                break;
            case NightLoadState.Patrol:

                break;
            case NightLoadState.Notice:

                break;
            case NightLoadState.Chase:

                break;
            case NightLoadState.JumpingAttack:

                break;
            case NightLoadState.Punch:

                break;
            case NightLoadState.Kick:

                break;
            case NightLoadState.Bite:

                break;
            case NightLoadState.KillInstantly:

                break;
            case NightLoadState.AbsorbCompanies:

                break;
            case NightLoadState.Metamorphosis:

                break;
            case NightLoadState.Die:

                break;
        }
    }
}
