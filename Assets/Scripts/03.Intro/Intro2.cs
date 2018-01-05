using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Intro2 : MonoBehaviour {

    [SerializeField] private float Timer;
    [SerializeField] private float MovieTime;

    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
  
    void Start ()
    {
		
	}
	
	
	void Update ()
    {
        Timer += Time.deltaTime;

        if (Timer > 66.0f)
            SceneManager.LoadScene("05.Intro3");

        if (Input.GetKey("escape"))
            SceneManager.LoadScene("05.Intro3");
    }
}
