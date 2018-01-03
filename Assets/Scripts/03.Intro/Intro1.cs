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
        StartCoroutine(IntroEnd());
	}
	
	
	void Update ()
    {
		if(Input.GetKey("escape"))
            SceneManager.LoadScene("04.Intro2");
	}

    IEnumerator IntroEnd()
    {
        yield return new WaitForSeconds(77.0f);
        SceneManager.LoadScene("04.Intro2");
    }
}
