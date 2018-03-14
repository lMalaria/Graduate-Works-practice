using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutSceneCamera : MonoBehaviour {

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
        Cursor.visible = false;
        thirdAngle = offsetPosition;
        velocity = Vector3.zero;
        isLookAt = false;
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
            transform.rotation = Quaternion.Euler(35,0,0);
    }
}
