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

    void Awake()
    {
        inputDelay = 0.3f;
        forwardVel = 5.0f;
        rotateVel = 100.0f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        currentCamera = Camera.main;
        animator = GetComponent<Animator>();

    }

    void Start()
    {
        targetRotation = transform.rotation;

        if (GetComponent<Rigidbody>())
            rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        forwardInput = Input.GetAxis("Vertical");
        turnInput = -Input.GetAxis("Horizontal");

        Turn();

        if(Input.GetKeyDown("f1"))
        {
            currentCamera.depth = -2;
        }

        if(Input.GetKeyDown("f2"))
        {
            currentCamera.depth = 0;
        }

        if (Input.GetMouseButton(0))
            animator.Play("demo_combat_shoot");

        if (Input.GetMouseButtonUp(0))
            animator.Play("demo_combat_idle");    
    }

    void FixedUpdate()
    {
        Run();
    }

    void Run()
    {
        if (Mathf.Abs(forwardInput) > inputDelay)
        {
            rb.velocity = transform.forward * forwardInput * forwardVel;
            animator.Play("demo_combat_run");
        }

        else
            rb.velocity = Vector3.zero;
    }

    void Turn()
    {
        targetRotation *= Quaternion.AngleAxis(rotateVel * turnInput * Time.deltaTime, -Vector3.up);

        transform.rotation = targetRotation;
    }
}
