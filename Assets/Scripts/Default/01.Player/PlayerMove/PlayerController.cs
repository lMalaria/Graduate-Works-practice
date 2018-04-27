using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    Animator anim;

    Rigidbody rb;

    //애니메이션 컨트롤러에서 불러올 것이라 public
    public float vertical;

    //애니메이션 컨트롤러에서 불러올 것이라 public
    public float horizontal;

    //애니메이션 컨트롤러에서 불러올 것이라 public
    public float movementAmount;

    private float rotationSpeed;

    [SerializeField]
    private Transform cameraTransform;

    public CharacterStatus characterStatus;

    [SerializeField]
    private Vector3 rotationDirection;

    [SerializeField]
    private Vector3 moveDirection;

    void Start ()
    {
        anim = GetComponent<Animator>();

        if (GetComponent<Rigidbody>())
            rb = GetComponent<Rigidbody>();

        rotationSpeed = 0.4f;
	}
	
	void Update ()
    {
        //vertical = Input.GetAxis("Vertical");
        //horizontal = Input.GetAxis("Horizontal");
        //movementAmount = Mathf.Clamp01(Mathf.Abs(vertical) + Mathf.Abs(horizontal));

        //anim.SetFloat("vertical", vertical, 0.15f, Time.deltaTime);

        //Vector3 moveDir = cameraTransform.forward * vertical;
        //moveDir = cameraTransform.right * horizontal;
        //moveDir.Normalize();
        //moveDirection = moveDir;
        //rotationDirection = cameraTransform.forward;
        //MoveUpdate();
    }

    public void MoveUpdate()
    {
        vertical = Input.GetAxis("Vertical");
        horizontal = Input.GetAxis("Horizontal");
        movementAmount = Mathf.Clamp01(Mathf.Abs(vertical) + Mathf.Abs(horizontal)); 

        Vector3 moveDir = cameraTransform.forward * vertical;
        moveDir += cameraTransform.right * horizontal;
        moveDir.Normalize();
        moveDirection = moveDir;
        rotationDirection = cameraTransform.forward;

        RotationNormal();
        characterStatus.isGround = MoveGround();
    }

    public void RotationNormal()
    {
        if(!characterStatus.isAiming)
        {
            rotationDirection = moveDirection;
        }

        Vector3 targetDir = rotationDirection;
        targetDir.y = 0;

        if (targetDir == Vector3.zero)
            targetDir = transform.forward;

        Quaternion lookDir = Quaternion.LookRotation(targetDir);
        Quaternion targetRot = Quaternion.Slerp(transform.rotation, lookDir, rotationSpeed);
        transform.rotation = targetRot;
    }

    public bool MoveGround()
    {
        Vector3 origin = transform.position;
        origin.y += 0.6f;

        Vector3 dir = -Vector3.up;

        float distance = 0.7f;

        RaycastHit hit;
        if(Physics.Raycast(origin, dir, out hit, distance))
        {
            Vector3 tp = hit.point;
            transform.position = tp;
            return true;
        }
        return false;
    }
}
