using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SWATPlayerController : MonoBehaviour {


    //에임 매니저에서 받아와야 하기 때문에 public
    public enum PlayerState
    {
        Idle = 0,
        Walk,
        Run,
        HoldingPistol,
        Shoot,
        Die
    }

    public PlayerState playerState;

    //좀비들의 스크립트에서 불러 올 것이기에 public
    public float playerHP;

    [SerializeField]
    private float inputDelay;

    [SerializeField]
    private float forwardVel;

    [SerializeField]
    private float rotateVel;

    [SerializeField]
    private float forwardInput;

    [SerializeField]
    private float turnInput;

    Animator animator;
    Quaternion targetRotation;
    Rigidbody rb;

    [SerializeField]
    private GameObject bloodParticle;

    void Awake()
    {
        playerHP = 100;
        inputDelay = 0.3f;
        forwardVel = 2.0f;
        rotateVel = 100.0f;

        animator = GetComponent<Animator>();
        playerState = PlayerState.Idle;
    }

    void Start()
    {
        targetRotation = transform.rotation;

        if (GetComponent<Rigidbody>())
            rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        //print(playerHP);
        forwardInput = Input.GetAxis("Vertical");
        turnInput = -Input.GetAxis("Horizontal");

        Turn();

        if (Input.GetMouseButton(1))
        {
            animator.SetBool("isWalking", false);
            animator.SetBool("isIdle", false);
            animator.SetBool("isHoldingPistol",true);

            if (Input.GetMouseButtonDown(0))
            {

            }

        }

        if (Input.GetMouseButtonUp(1))
        {
            animator.SetBool("isIdle", true);
            animator.SetBool("isHoldingPistol", false);
        }

        if (playerHP <= 0)
            Destroy(this.gameObject);

    }

    void FixedUpdate()
    {
        Walk();
        Run();
    }

    void Walk()
    {
        playerState = PlayerState.Walk;

        if (Mathf.Abs(forwardInput) > inputDelay)
        {
            animator.SetBool("isWalking", true);
            animator.SetBool("isIdle", false);
            rb.velocity = transform.forward * forwardInput * forwardVel;
        }

        else
        {
            animator.SetBool("isIdle", true);
            animator.SetBool("isRunning", false);
            animator.SetBool("isWalking", false);

            rb.velocity = Vector3.zero;
        }
        
    }

    void Run()
    {
        playerState = PlayerState.Run;

        if (Mathf.Abs(forwardInput) > inputDelay && Input.GetKey("left ctrl"))
        {

            animator.SetBool("isRunning", true);
            animator.SetBool("isWalking", false);
            animator.SetBool("isHoldingPistol", false);
            animator.SetBool("isShooting", false);
            animator.SetBool("isIdle", false);
            rb.velocity = transform.forward * forwardInput * forwardVel * 1.8f;
        }
    }

    void Turn()
    {
        targetRotation *= Quaternion.AngleAxis(rotateVel * turnInput * Time.deltaTime, -Vector3.up);

        transform.rotation = targetRotation;
    }

    public void IsHurt(float damageTaken)
    {
        playerHP -= damageTaken;
    }
}
