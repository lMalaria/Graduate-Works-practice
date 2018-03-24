using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour {

    enum Weapon
    {
        BareHand = 0,
        Pistol,
        M4,
        AK47,
        ScolPion,
        UMP45
    }

    [SerializeField]
    private GameObject[] weapons;

    [SerializeField]
    private bool[] isEquipped;

    private GameObject weaponsEquipped;

    [SerializeField]
    private float switchDelay;

    private bool isSwitching;

    private int index;

	void Start ()
    {
        SwitchWeapons((int)Weapon.Pistol);
	}

	void Update ()
    {
        if(Input.GetKeyDown( ((int)Weapon.Pistol).ToString()) )
        {
            StartCoroutine(SwitchAfterDelay((int)Weapon.Pistol));
        }

        if(Input.GetKeyDown("2"))
        {
            StartCoroutine(SwitchAfterDelay(2));
        }

        if(Input.GetKeyDown("3"))
        {
            StartCoroutine(SwitchAfterDelay(3));
        }

        if(Input.GetKeyDown("4"))
        {
            StartCoroutine(SwitchAfterDelay(4));
        }

        if(Input.GetKeyDown("5"))
        {
            StartCoroutine(SwitchAfterDelay(5));
        }

		if(Input.GetAxis("Mouse ScrollWheel") > 0 && !isSwitching )
        {
            index++;

            if (index > weapons.Length)
                index = 0;

            StartCoroutine(SwitchAfterDelay(index));
        }

        else if(Input.GetAxis("Mouse ScrollWheel") < 0 && !isSwitching)
        {
            index--;

            if (index <= 0)
                index = weapons.Length - 1;
        }
	}

    IEnumerator SwitchAfterDelay(int newIndex)
    {
        isSwitching = true;

        yield return new WaitForSeconds(switchDelay);

        isSwitching = false;
        SwitchWeapons(newIndex);
    }

    void SwitchWeapons(int newIndex)
    {
        for (int i = 0; i < weapons.Length; i++)
            weapons[i].SetActive(false);
        
        weapons[newIndex].SetActive(true);
    }

    int String2Weapon(string name)
    {
        return (int)Enum.Parse(typeof(Weapon), name);
    }

    string Weapon2String(Weapon weapon)
    {
        return weapon.ToString();
    }

    //string Int2String(int intGointToBeChanged)
    //{
    //    return intGointToBeChanged.ToString();
    //}
}
