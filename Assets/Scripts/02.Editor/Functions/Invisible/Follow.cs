using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour {

    private float distance = 20.0f;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 MousePosition = Input.mousePosition + new Vector3(0.0f,20.0f,0.0f);
        MousePosition.z = distance;
        transform.position = Camera.main.ScreenToWorldPoint(MousePosition);
    }
}
