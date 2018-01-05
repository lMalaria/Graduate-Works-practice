using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Intro1 : MonoBehaviour {

    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

	void Start ()
    {
        Invoke("changeToSecondIntro", 77);
	}

	void Update ()
    {
		if(Input.GetKey("escape"))
            SceneManager.LoadScene("04.Intro2");
	}

    private void changeToSecondIntro()
    {
        SceneManager.LoadScene("04.Intro2");
    }
}
