using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawnManager : MonoBehaviour {

    [SerializeField]
    private GameObject item;

    [SerializeField]
    private GameObject itemInstance;

    private Vector3 itemPosition;

	void Start ()
    {
        itemPosition = item.transform.position;
        InvokeRepeating("CheckItem", 1, 3);
	}

	void Update ()
    {
        if (Input.GetKeyDown("q"))
            Destroy(item);
	}

    void SpawnItem(Vector3 spawnedPosition)
    {
        Instantiate(item, spawnedPosition, Quaternion.identity);
    }

    string Item2String(GameObject item)
    {
        var itemName = item.ToString();

        return GameObject.Find(itemName).ToString();
    }

    void CheckItem()
    {
        
        GameObject colaInstance;

        //if (GameObject.Find("Cola Can") == null)
        if (item == null)
        {
            colaInstance = Instantiate(item, itemPosition, Quaternion.identity);
        }
        

    }
}
