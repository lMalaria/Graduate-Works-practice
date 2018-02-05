using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPlayer : MonoBehaviour {

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

    private Camera currentCamera;

    Animator animator;
    Quaternion targetRotation;
    Rigidbody rb;
    ZombieControll zombieControll;

    private GameObject zombieInstance;

    [SerializeField]
    private Texture2D cursorTexture;

    [SerializeField]
    private CursorMode cursorMode;

    [SerializeField]
    private Vector2 hotSpot;

    void Awake()
    {
        inputDelay = 0.3f;
        forwardVel = 2.0f;
        rotateVel = 100.0f;

        Cursor.visible = false;
        currentCamera = Camera.main;

        animator = GetComponent<Animator>();

        zombieControll = GetComponent<ZombieControll>();
        zombieInstance = GameObject.Find("Zombie");
        cursorMode = CursorMode.Auto;
        hotSpot = Vector2.zero;


    }

    //void OnMouseEnter()
    //{
    //    Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
    //}

    //void OnMouseExit()
    //{
    //    Cursor.SetCursor(null, Vector2.zero, cursorMode);
    //}

    void Start()
    {
        targetRotation = transform.rotation;

        if (GetComponent<Rigidbody>())
            rb = GetComponent<Rigidbody>();

        zombieControll = zombieInstance.GetComponent<ZombieControll>();

    }

    void Update()
    {

        print(zombieControll.zombieHP);

        forwardInput = Input.GetAxis("Vertical");
        turnInput = -Input.GetAxis("Horizontal");

        Turn();

        if(Input.GetKeyDown("f1"))
            currentCamera.depth = -2;
        

        if(Input.GetKeyDown("f2"))
            currentCamera.depth = 0;

        if (Input.GetMouseButton(1))
        {
            Cursor.visible = true;
            Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);

            animator.SetBool("isWalking", false);
            animator.SetBool("isIdle", false);
            animator.SetBool("isHoldingPistol",true);
            //cameraFollow.offsetPosition = new Vector3(1,2.2f,-1.85f);

            if (Input.GetMouseButtonDown(0))
            {

                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (!Physics.Raycast(ray, out hit, 150)) return;

                if(hit.collider.tag == "Enemy")
                    zombieControll.zombieHP = zombieControll.zombieHP - 5;
            }

        }

        if (Input.GetMouseButtonUp(1))
        {
            animator.SetBool("isIdle", true);
            animator.SetBool("isHoldingPistol", false);
            Cursor.visible = false;
            Cursor.SetCursor(null, Vector2.zero, cursorMode);
        }

    }

    void FixedUpdate()
    {
        Walk();
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

    void Turn()
    {
        targetRotation *= Quaternion.AngleAxis(rotateVel * turnInput * Time.deltaTime, -Vector3.up);

        transform.rotation = targetRotation;
    }
}
