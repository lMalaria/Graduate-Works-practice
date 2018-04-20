using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectableItems : MonoBehaviour {

    private BoxCollider boxCollider;

    void Start ()
    {
        boxCollider = GetComponent<BoxCollider>();
    }
	
	void Update ()
    {
		
	}

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            boxCollider.isTrigger = true;
            //Destroy(this.gameObject);
        }
    }

}
