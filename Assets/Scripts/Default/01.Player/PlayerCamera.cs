using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour {

    [SerializeField] private float cameraMoveSpeed;
    //[SerializeField] private GameObject CameraFollowObj;
    [SerializeField] private float ClampAngle;
    [SerializeField] private float InputSensitiviy;
    [SerializeField] private float mouseX;
    [SerializeField] private float mouseY;
    [SerializeField] private float rotX;
    [SerializeField] private float rotY;
    [SerializeField] private bool LookMode;
    [SerializeField] private Vector3 rotRest;


    void Awake()
    {

        cameraMoveSpeed = 120.0f;
        //CameraFollowObj = GameObject.Find("CameraFollow");
        ClampAngle = 80.0f;
        InputSensitiviy = 150.0f;
        rotX = 0.0f;
        rotY = 0.0f;
        LookMode = false;

    }



    void Start()
    {

        Vector3 rot = transform.localRotation.eulerAngles;
        rotRest = transform.localRotation.eulerAngles;
        rotX = rot.x;
        rotY = rot.y;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }

    void Update()
    {

        mouseX = Input.GetAxis("Horizontal");
        mouseY = -Input.GetAxis("Vertical");

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            LookMode = true;
        }

        if (Input.GetKeyUp(KeyCode.Tab))
        {
            LookMode = false;
        }

        if (Input.GetKey(KeyCode.T))
        {
            rotX = rotRest.x;
            rotY = rotRest.y;
        }

        if (LookMode)
        {

            rotY += mouseX * InputSensitiviy * Time.deltaTime;
            rotX += mouseY * InputSensitiviy * Time.deltaTime;

            rotX = Mathf.Clamp(rotX, -ClampAngle, ClampAngle);

            //Quaternion localRotation = Quaternion.Euler(rotX, rotY, 0.0f);

            //transform.rotation = localRotation;

        }



        Quaternion localRotation = Quaternion.Euler(rotX, rotY, 0.0f);

        transform.rotation = localRotation;

    }



    void LateUpdate()
    {

        CameraUpdater();

    }



    void CameraUpdater()
    {

        //Transform Target = CameraFollowObj.transform;



        float step = cameraMoveSpeed * Time.deltaTime;

        //transform.position = Vector3.MoveTowards(transform.position, Target.position, step);

    }
}
