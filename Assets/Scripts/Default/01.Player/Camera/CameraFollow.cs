using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    //[SerializeField]
    //private Transform target;

    //private Vector3 velocity = Vector3.zero;

    //private float cameraHeight;

    //private float cameraDistance;

    //private float inputHorizontal;

    //Quaternion cameraRotation;

    //void Awake()
    //{

    //    cameraHeight = 0.5f;
    //    cameraDistance = -1.0f;

    //}

    //void Start()
    //{
    //    transform.position = target.position + new Vector3(0, cameraHeight, cameraDistance);
    //    cameraRotation = transform.rotation;
    //}

    //void Update()
    //{
    //    if (transform.position.z < target.position.z - 0.5f)
    //        transform.position = Vector3.SmoothDamp(transform.position, target.position + new Vector3(0, cameraHeight, cameraDistance), ref velocity, 0.2f);
    //    else if (transform.position.z > target.position.z - 10f)
    //        transform.position = Vector3.SmoothDamp(transform.position, target.position + new Vector3(0, cameraHeight, cameraDistance), ref velocity, 0.2f);

    //    //inputHorizontal = Input.GetAxis("Horizontal");

    //    //cameraRotation *= Quaternion.AngleAxis(100.0f * inputHorizontal * Time.deltaTime, Vector3.up);

    //    //transform.rotation = cameraRotation;

    //}

    [SerializeField]
    private Transform target;

    public Vector3 offsetPosition;

    [SerializeField]
    private Space offsetPositionSpace;

    [SerializeField]
    private bool isLookAt;

    private Vector3 thirdAngle;

    private Vector3 velocity;

    void Awake()
    {
        thirdAngle = offsetPosition;
        velocity = Vector3.zero;
    }

    void LateUpdate()
    {
        if (target == null) return;

        //World 혹은 Self를 선택할 수 있는데 여기서 월드는 아마 쓰지 않을 것 같다 cf)나중에 쓸 일이 생길 것 같아서 따로 만듬
        if (offsetPositionSpace == Space.Self)
            transform.position = target.TransformPoint(offsetPosition);
        else
            transform.position = target.position + offsetPosition;

        //나중에 필요 하다고 생각하여 만들어 놓은 LookAt 함수 cf)캐릭터 주위를 도는 카메라 생성
        if (isLookAt)
            transform.LookAt(target);
        else
            transform.rotation = target.rotation;

        if (Input.GetMouseButton(1))
            offsetPosition = Vector3.Slerp(offsetPosition, new Vector3(0.5f, 1.6f, -1.3f), 5 * Time.deltaTime);//new Vector3(1, 2.2f, -1.85f);

        if (Input.GetMouseButtonUp(1))
            //offsetPosition = Vector3.Slerp(new Vector3(0.4f, 1.5f, -1.2f), thirdAngle + new Vector3(0,0,-5), 5 * Time.deltaTime);
            offsetPosition = thirdAngle;
    }
}
