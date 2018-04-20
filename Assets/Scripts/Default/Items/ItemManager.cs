using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour {

    private GameObject[] Items;

	// Use this for initialization
	void Start ()
    {
        Items = GameObject.FindGameObjectsWithTag("Item");
        
        for(int i = 0; i< Items.Length; i++)
        {
            Items[i].AddComponent<CollectableItems>();
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
