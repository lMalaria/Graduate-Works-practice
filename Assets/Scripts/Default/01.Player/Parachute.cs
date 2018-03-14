using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parachute : MonoBehaviour {

    [SerializeField]
    private GameObject cameraObject;

    Rigidbody rigidbody;

    private float dialogueEndTime;

    //[SerializeField]
    //Camera[] cameras;

    void Awake()
    {
        //cameraObject = GameObject.Find("Camera");
        rigidbody = GetComponent<Rigidbody>();

        // 연출을 위해서 3인칭 카메라와 중력적용을 꺼 놓은 채 시작 
        cameraObject.GetComponent<CameraFollow>().enabled = false;
        cameraObject.GetComponent<CutSceneCamera>().enabled = false;
        rigidbody.useGravity = false;
        rigidbody.constraints = RigidbodyConstraints.FreezeRotation; // 충돌시 물체가 돌아가지 않게 설정
    }

	void Start ()
    {
        //this.gameObject.transform.Rotate(90, 0, 0);
    }
	
	void Update ()
    {
        dialogueEndTime += Time.deltaTime;

        if (dialogueEndTime > 13 /*dialogueEnd*/ )
        {
            cameraObject.GetComponent<CutSceneCamera>().enabled = true;
            //rigidbody.drag = 1;
            rigidbody.useGravity = true;           
        }

        if (this.gameObject.transform.position.y < 400)
            rigidbody.drag = 1;

        if (this.gameObject.transform.position.y < 20)
        {
            rigidbody.drag = 0;
            cameraObject.GetComponent<CutSceneCamera>().enabled = false;
            cameraObject.GetComponent<CameraFollow>().enabled = true;
        }

	}
}
