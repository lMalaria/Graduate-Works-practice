using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {

    [SerializeField] private float inputDelay;
    [SerializeField] private float forwardVel;
    [SerializeField] private float rotateVel;
    [SerializeField] private float forwardInput;
    [SerializeField] private float turnInput;
    //[SerializeField] private Animator animator;

    Quaternion targetRotation;
    Rigidbody rb;





    //public Quaternion TargetRotation

    //{

    //    get {return targetRotation; }

    //}



    void Awake()
    {
        inputDelay = 0.1f;
        forwardVel = 12.0f;
        rotateVel = 100.0f;
        //animator = GetComponent<Animator>();
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

        }

        else

        {

            rb.velocity = Vector3.zero;

        }

    }



    void Turn()
    {

        targetRotation *= Quaternion.AngleAxis(rotateVel * turnInput * Time.deltaTime, -Vector3.up);

        transform.rotation = targetRotation;

    }
}
