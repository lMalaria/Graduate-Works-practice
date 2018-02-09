using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothCamera : MonoBehaviour {

    [SerializeField]
    private Transform car;

    [SerializeField]
    private float distance;

    [SerializeField]
    private float height;

    [SerializeField]
    private float rotationDamping;

    [SerializeField]
    private float heightDamping;

    [SerializeField]
    private float zoomRatio;

    //FOV = Field Of View
    [SerializeField]
    private float defaultFOV;

    private float rotationVector;

    void FixedUpdate()
    {
        Vector3 localVelocity = car.InverseTransformDirection(car.GetComponent<Rigidbody>().velocity);

        if (localVelocity.z < -0.5f)
            rotationVector = car.eulerAngles.y + 100;
        else
            rotationVector = car.eulerAngles.y;

        float acceleration = car.GetComponent<Rigidbody>().velocity.magnitude;
        Camera.main.fieldOfView = defaultFOV + acceleration * zoomRatio * Time.deltaTime;
    }

    void LateUpdate()
    {
        float wantedAngle = rotationVector;

        float wantedHeight = car.position.y + height;
        float myAngle = transform.eulerAngles.y;
        float myHeight = transform.position.y;

        myAngle = Mathf.LerpAngle(myAngle, wantedAngle, rotationDamping * Time.deltaTime);
        myHeight = Mathf.LerpAngle(myHeight, wantedHeight, rotationDamping * Time.deltaTime);

        Quaternion currentRotation = Quaternion.Euler(0, myAngle, 0);

        transform.position = car.position;
        transform.position = currentRotation * Vector3.forward * distance;

        Vector3 temp = transform.position;
        temp.y = myHeight;
        transform.position = temp;

        transform.LookAt(car);
    }

	void Start () {
		
	}
	
	void Update () {
		
	}
}
