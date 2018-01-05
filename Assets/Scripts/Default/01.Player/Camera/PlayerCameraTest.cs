using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraTest : MonoBehaviour {

    [SerializeField] private Transform target;
    [SerializeField] private Vector3 defaultDistance;
    [SerializeField] private float distanceDamp;
    [SerializeField] private float rotationDamp;
    [SerializeField] private Transform myTransform;
    [SerializeField] private Vector3 velocity;

    void Awake()
    {
        defaultDistance = new Vector3(0.0f, 2.0f, -10f);
        distanceDamp = 10.0f;
        rotationDamp = 10.0f;
        myTransform = transform;
        velocity = Vector3.one;
    }

	void Start ()
    {
       
	}
	
	void Update ()
    {
		
	}

    void LateUpdate()
    {
        SetCameraSmooth();
        
    }

    void SetCameraSmooth()
    {
        Vector3 pos = target.position + (target.rotation * defaultDistance);
        Vector3 currentPos = Vector3.SmoothDamp(myTransform.position, pos, ref velocity, distanceDamp);
        myTransform.position = currentPos;
        myTransform.LookAt(target, target.up);
    }
}
