using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LogoEnd : MonoBehaviour {
    [SerializeField] private float Timer;
    [SerializeField] private float MovieTime;
	// Use this for initialization

    void Awake()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Timer = 0.0f;
        MovieTime = 0.0f;
    }

	void Start ()
    {

	}
	
	// Update is called once per frame
	void Update ()
    {
        Timer += Time.deltaTime;

        if (Timer > 8.5f)
        {
            Cursor.visible = true;
            SceneManager.LoadScene("01.Title");
            
        }

        if (Input.GetKey("escape"))
        {
            Cursor.visible = true;
            SceneManager.LoadScene("01.Title");
            
        }


    }
}
