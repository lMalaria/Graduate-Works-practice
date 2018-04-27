using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInput : MonoBehaviour {

    public CharacterStatus characterStatus;

    [SerializeField]
    private bool debugAiming;

    [SerializeField]
    private bool isAiming;

	public void UpdateInput ()
    {
        if (!debugAiming)
            characterStatus.isAiming = Input.GetMouseButton(1);
        else
            characterStatus.isAiming = isAiming;
	}
}
