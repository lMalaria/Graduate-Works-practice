using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterIK : MonoBehaviour {

    Animator anim;

    [SerializeField]
    private PlayerController playerController;

    [SerializeField]
    private CharacterInventory characterInventory;

    [SerializeField]
    private CharacterStatus characterStatus;

    [SerializeField]
    private Transform targetLook;

    [SerializeField]
    private Transform leftHand;

    [SerializeField]
    private Transform targetOfLeftHand;

    [SerializeField]
    private Transform rightHand;

    [SerializeField]
    private Quaternion leftHandRotation;

    [SerializeField]
    private float rightHandWeight;

    [SerializeField]
    private Transform shoulder;

    [SerializeField]
    private Transform aimPivot;

    void Start ()
    {
        anim = GetComponent<Animator>();
        shoulder = anim.GetBoneTransform(HumanBodyBones.RightShoulder).transform;

        aimPivot = new GameObject().transform;
        aimPivot.name = "aim Pivot";
        aimPivot.transform.parent = transform;

        rightHand = new GameObject().transform;
        rightHand.name = "right Hand";
        rightHand.transform.parent = aimPivot;

        leftHand = new GameObject().transform;
        leftHand.name = "left Hand";
        leftHand.transform.parent = aimPivot;

        rightHand.localPosition = characterInventory.firstWeapon.rightHandPos;
        Quaternion rightRotation = Quaternion.Euler(characterInventory.firstWeapon.rightHandPos);
        rightHand.rotation = rightRotation;
         
	}
	
	void Update ()
    {
        leftHandRotation = leftHand.rotation;
        leftHand.position = targetOfLeftHand.position;
        leftHand.rotation = leftHandRotation;
	}
}
