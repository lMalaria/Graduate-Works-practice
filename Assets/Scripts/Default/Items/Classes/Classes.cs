using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Classes : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

[System.Serializable]
public class Item
{
    public string name;
    public string description;
    public bool stackable;
    public int value;
    public GameObject model;
}

[System.Serializable]
public class Weapon : Item
{
    public int damage;

    public Weapon(string name, string description, bool stackable, int damage, int value, GameObject model)
    {
        this.name = name;
        this.description = description;
        this.stackable = stackable;
        this.damage = damage;
        this.value = value;
        this.model = model;
    }
}

[System.Serializable]
public class Ammo : Item
{
    public Ammo(string name, string description, bool stackable, int value, GameObject model)
    {
        this.name = name;
        this.description = description;
        this.stackable = stackable;
        this.value = value;
        this.model = model;
    }
}

[System.Serializable]
public class Armor : Item
{
    public int armorValue;

    public Armor(string name, string description, bool stackable, int armorValue, int value, GameObject model)
    {
        this.name = name;
        this.description = description;
        this.stackable = stackable;
        this.armorValue = armorValue;
        this.value = value;
        this.model = model;
    }
}

[System.Serializable]
public class Food : Item
{
    public int staminaValue;

    public Food(string name, string description, bool stackable, int staminaValue ,int value, GameObject model)
    {
        this.name = name;
        this.description = description;
        this.stackable = stackable;
        this.staminaValue = staminaValue;
        this.value = value;
        this.model = model;
    }
}

[System.Serializable]
public class Potion : Item
{
    public int healValue;

    public Potion(string name, string description, bool stackable, int healValue, int value, GameObject model)
    {
        this.name = name;
        this.description = description;
        this.stackable = stackable;
        this.healValue = healValue;
        this.value = value;
        this.model = model;
    }
}

[System.Serializable]
public class Material : Item
{
    public string typeofMaterial;

    public Material(string name, string description, bool stackable, string typeofMaterial , int value, GameObject model)
    {
        this.name = name;
        this.description = description;
        this.stackable = stackable;
        this.typeofMaterial = typeofMaterial;
        this.value = value;
        this.model = model;
    }
}