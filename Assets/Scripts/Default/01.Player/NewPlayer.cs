using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPlayer : MonoBehaviour {

    [SerializeField] private float inputDelay;
    [SerializeField] private float forwardVel;
    [SerializeField] private float rotateVel;
    [SerializeField] private float forwardInput;
    [SerializeField] private float turnInput;

    //[SerializeField] private Animator animator;

    Quaternion targetRotation;
    Rigidbody rb;



    void Awake()
    {
        inputDelay = 0.3f;
        forwardVel = 5.0f;
        rotateVel = 100.0f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
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

    enum CurrentWeaponState
    {
        HAND_GUN = 0,
        SHOT_GUN,
        MACHINE_GUN,
        SNIPER_RIFLE,
        BAZOOKA
    }

    enum PlayerAnimationState
    {
        IDLE,
        WALK,
        RUN,
        AIMING,
        RELOADING,
        SHOOT,
        HIT,
        DEAD

    }
}
