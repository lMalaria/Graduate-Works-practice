using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour {

    Animator anim;

    public PlayerController playerController;

    public CharacterStatus characterStatus;

	void Start ()
    {
        anim = GetComponent<Animator>();
	}
	

	public void UpdateAnimation()
    {
        anim.SetBool("running", characterStatus.isRunning);
        anim.SetBool("aiming", characterStatus.isAiming);

        if (!characterStatus.isAiming)
            AnimationNormal();
        else
            AnimationAiming();
    }

    void AnimationNormal()
    {
        anim.SetFloat("vertical", playerController.movementAmount, 0.15f, Time.deltaTime);
    }

    void AnimationAiming()
    {
        float v = playerController.vertical;
        float h = playerController.horizontal;

        anim.SetFloat("vertical", v, 0.15f, Time.deltaTime);
        anim.SetFloat("horizontal", h, 0.15f, Time.deltaTime);
    }
}
