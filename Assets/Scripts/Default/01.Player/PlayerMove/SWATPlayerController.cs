using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SWATPlayerController : MonoBehaviour {

    private enum PlayerStates
    {
        Idle = 0,
        Walk,
        Run,
        HoldingPistol,
        Shoot,
        Die
    }

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
    ZombieController zombieController;

    private GameObject zombieInstance;

    [SerializeField]
    private GameObject crossHair;

    private Vector2 crossHairPosition;

    public bool isAutoTargetingModeOn;

    [SerializeField]
    private GameObject bloodParticle;

    void Awake()
    {
        inputDelay = 0.3f;
        forwardVel = 2.0f;
        rotateVel = 100.0f;

        Cursor.visible = false;

        animator = GetComponent<Animator>();

        zombieController = GetComponent<ZombieController>();
        zombieInstance = GameObject.Find("CopZombie");

        isAutoTargetingModeOn = false;

    }

    void Start()
    {
        crossHair.SetActive(false);
        targetRotation = transform.rotation;

        if (GetComponent<Rigidbody>())
            rb = GetComponent<Rigidbody>();

        zombieController = zombieInstance.GetComponent<ZombieController>();

    }

    void Update()
    {
        crossHairPosition = new Vector3(zombieInstance.transform.position.x, zombieInstance.transform.position.y);

        print("좀비 HP" +" "+zombieController.zombieHP);
        print("크로스헤어 위치:" + " " + crossHairPosition);

        if(isAutoTargetingModeOn == false)
            crossHair.transform.position = Input.mousePosition;

        if (isAutoTargetingModeOn == true)
        {
            crossHair.transform.position = crossHairPosition;
        }

        forwardInput = Input.GetAxis("Vertical");
        turnInput = -Input.GetAxis("Horizontal");

        Turn();

        if (Input.GetMouseButton(1))
        {
            crossHair.SetActive(true);

            animator.SetBool("isWalking", false);
            animator.SetBool("isIdle", false);
            animator.SetBool("isHoldingPistol",true);

            if (Input.GetMouseButtonDown(0))
            {

                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (!Physics.Raycast(ray, out hit, 150)) return;

                if (hit.collider.tag == "Enemy")
                {
                    zombieController.zombieHP = zombieController.zombieHP - 5;
                    Instantiate(bloodParticle, hit.point, Quaternion.identity);
                }
            }

        }

        if (Input.GetMouseButtonUp(1))
        {
            crossHair.SetActive(false);
            animator.SetBool("isIdle", true);
            animator.SetBool("isHoldingPistol", false);
            Cursor.visible = false;
        }

    }

    void FixedUpdate()
    {
        Walk();
        Run();
    }

    void Walk()
    {
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
        if (Mathf.Abs(forwardInput) > inputDelay && Input.GetKey("left ctrl"))
        {

            animator.SetBool("isRunning", true);
            animator.SetBool("isWalking", false);
            animator.SetBool("isHoldingPistol", false);
            animator.SetBool("isShooting", false);
            animator.SetBool("isIdle", false);
            rb.velocity = transform.forward * forwardInput * forwardVel;
        }
    }

    void Turn()
    {
        targetRotation *= Quaternion.AngleAxis(rotateVel * turnInput * Time.deltaTime, -Vector3.up);

        transform.rotation = targetRotation;
    }
}
