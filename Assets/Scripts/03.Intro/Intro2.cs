using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Intro2 : MonoBehaviour {

    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
  
    void Start ()
    {
        Invoke("changeToThirdIntro", 66);
	}
	
	void Update ()
    {
        if(Input.GetKey("escape"))
            SceneManager.LoadScene("05.Intro3");
    }

    private void changeToThirdIntro()
    {
        SceneManager.LoadScene("05.Intro3");
    }
}
