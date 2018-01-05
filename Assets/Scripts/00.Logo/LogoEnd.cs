using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LogoEnd : MonoBehaviour {
 
    void Awake()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

	void Start ()
    {
        Invoke("changeToTitleScene",9);
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKey("escape"))
        {
            Cursor.visible = true;
            SceneManager.LoadScene("01.Title");     
        }
    }

    private void changeToTitleScene()
    {
        print("change to Title Scene");
        Cursor.visible = true;
        SceneManager.LoadScene("01.Title");
    }
}
