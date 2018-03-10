using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using debug = UnityEngine.Debug;

public class SkeletonController : MonoBehaviour {

    public enum SkeletonState
    {
        Idle = 0,
        Wander,
        Notice,
        Chase,
        Fight,
        Confront,
        QuickSwing,
        HeavySwing,
        Die,
        Resurrection
    }

    SkeletonState skeletonState;

    private Stopwatch sw = new Stopwatch();

    private GameObject player;

    Animator animator;

    private float skeletonHP;

    void Awake()
    {
        player = GameObject.Find("SWAT");
        animator = GetComponent<Animator>();

        skeletonHP = 100;
    }

	void Start ()
    {
        sw.Reset();
        skeletonState = SkeletonState.Idle;
    }
	
	void Update ()
    {
        SkeletonFSM();
	}

    void SkeletonFSM()
    {
        Vector3 direction = player.transform.position - this.transform.position;
        direction.y = 0;

        switch(skeletonState)
        {
            case SkeletonState.Idle:
                animator.SetBool("isIdle", true);
                animator.SetBool("isRunning", false);
                animator.SetBool("isWalking", false);
                animator.SetBool("isWalkingBack", false);
                animator.SetBool("isWalkingLeft", false);
                animator.SetBool("isWalkingRight", false);
                animator.SetBool("isSwingQuickly", false);
                animator.SetBool("isSwingHeavily", false);
                animator.SetBool("isDead", false);
                animator.SetBool("isResurrection", false);

                sw.Start();

                if (sw.ElapsedMilliseconds > 3000)
                    skeletonState = SkeletonState.Wander;

                if (Vector3.Distance(player.transform.position, this.transform.position) < 7)
                        skeletonState = SkeletonState.Notice;
                break;
            case SkeletonState.Wander:
                animator.SetBool("isIdle", false);
                animator.SetBool("isRunning", false);
                animator.SetBool("isWalking", true);
                animator.SetBool("isWalkingBack", false);
                animator.SetBool("isWalkingLeft", false);
                animator.SetBool("isWalkingRight", false);
                animator.SetBool("isSwingQuickly", false);
                animator.SetBool("isSwingHeavily", false);
                animator.SetBool("isDead", false);
                animator.SetBool("isResurrection", false);

                sw.Start();

                if (sw.ElapsedMilliseconds > 6000)
                {
                    sw.Reset();
                    skeletonState = SkeletonState.Idle;
                }

                if (Vector3.Distance(player.transform.position, this.transform.position) < 7)
                {
                    sw.Reset();
                    skeletonState = SkeletonState.Notice;
                }

                break;
            case SkeletonState.Notice:
                direction = player.transform.position - this.transform.position;
                this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), 0.1f);

                animator.SetBool("isIdle", false);
                animator.SetBool("isRunning", false);
                animator.SetBool("isWalking", false);
                animator.SetBool("isWalkingBack", false);
                animator.SetBool("isWalkingLeft", false);
                animator.SetBool("isWalkingRight", false);
                animator.SetBool("isSwingQuickly", false);
                animator.SetBool("isSwingHeavily", false);
                animator.SetBool("isDead", false);
                animator.SetBool("isResurrection", false);

                if (sw.ElapsedMilliseconds > 2000)
                    skeletonState = SkeletonState.Chase;
                

                break;
            case SkeletonState.Chase:
                direction = player.transform.position - this.transform.position;
                this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), 0.1f);

                animator.SetBool("isIdle", false);
                animator.SetBool("isRunning", true);
                animator.SetBool("isWalking", false);
                animator.SetBool("isWalkingBack", false);
                animator.SetBool("isWalkingLeft", false);
                animator.SetBool("isWalkingRight", false);
                animator.SetBool("isSwingQuickly", false);
                animator.SetBool("isSwingHeavily", false);
                animator.SetBool("isDead", false);
                animator.SetBool("isResurrection", false);

                if (Vector3.Distance(player.transform.position, this.transform.position) < 2)
                {
                    sw.Reset();
                    skeletonState = SkeletonState.Confront;
                }

                break;
            case SkeletonState.Confront:
                direction = player.transform.position - this.transform.position;
                this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), 0.1f);

                animator.SetBool("isIdle", false);
                animator.SetBool("isRunning", false);
                animator.SetBool("isWalking", false);
                animator.SetBool("isWalkingBack", false);
                animator.SetBool("isWalkingLeft", false);
                animator.SetBool("isWalkingRight", false);
                animator.SetBool("isSwingQuickly", false);
                animator.SetBool("isSwingHeavily", false);
                animator.SetBool("isDead", false);
                animator.SetBool("isResurrection", false);

                if(sw.ElapsedMilliseconds > 2000)
                {
                    int checkBehaviorNum = Random.Range(1, 2);

                    if (checkBehaviorNum == 1)
                    {
                        sw.Reset();
                        skeletonState = SkeletonState.QuickSwing;
                    }

                    if (checkBehaviorNum == 2)
                    {
                        sw.Reset();
                        skeletonState = SkeletonState.HeavySwing;
                    }
                }

                break;
            case SkeletonState.QuickSwing:
                direction = player.transform.position - this.transform.position;
                this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), 0.1f);

                animator.SetBool("isIdle", false);
                animator.SetBool("isRunning", false);
                animator.SetBool("isWalking", false);
                animator.SetBool("isWalkingBack", false);
                animator.SetBool("isWalkingLeft", false);
                animator.SetBool("isWalkingRight", false);
                animator.SetBool("isSwingQuickly", true);
                animator.SetBool("isSwingHeavily", false);
                animator.SetBool("isDead", false);
                animator.SetBool("isResurrection", false);


                break;
            case SkeletonState.HeavySwing:
                direction = player.transform.position - this.transform.position;
                this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), 0.1f);

                animator.SetBool("isIdle", false);
                animator.SetBool("isRunning", false);
                animator.SetBool("isWalking", false);
                animator.SetBool("isWalkingBack", false);
                animator.SetBool("isWalkingLeft", false);
                animator.SetBool("isWalkingRight", false);
                animator.SetBool("isSwingQuickly", false);
                animator.SetBool("isSwingHeavily", true);
                animator.SetBool("isDead", false);
                animator.SetBool("isResurrection", false);

                break;
            case SkeletonState.Die:
                animator.SetBool("isIdle", false);
                animator.SetBool("isRunning", false);
                animator.SetBool("isWalking", false);
                animator.SetBool("isWalkingBack", false);
                animator.SetBool("isWalkingLeft", false);
                animator.SetBool("isWalkingRight", false);
                animator.SetBool("isSwingQuickly", false);
                animator.SetBool("isSwingHeavily", false);
                animator.SetBool("isDead", true);
                animator.SetBool("isResurrection", false);

                break;
            case SkeletonState.Resurrection:
                animator.SetBool("isIdle", false);
                animator.SetBool("isRunning", false);
                animator.SetBool("isWalking", false);
                animator.SetBool("isWalkingBack", false);
                animator.SetBool("isWalkingLeft", false);
                animator.SetBool("isWalkingRight", false);
                animator.SetBool("isSwingQuickly", false);
                animator.SetBool("isSwingHeavily", false);
                animator.SetBool("isDead", false);
                animator.SetBool("isResurrection", true);

                break;
        }
    }

    public void IsBeingDamaged(float weaponDmg)
    {
        skeletonHP -= weaponDmg;
    }
}
