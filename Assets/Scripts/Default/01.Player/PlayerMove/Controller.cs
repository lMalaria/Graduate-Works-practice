using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {

    public PlayerController playerController;
    public CharacterAnimation characterAnimation;
    public CharacterInput characterInput;

	void Start ()
    {
		
	}
	
	void Update ()
    {
        playerController.MoveUpdate();
        characterAnimation.UpdateAnimation();
        characterInput.UpdateInput();
    }

    public void FixedUpdate()
    {
    }
}
