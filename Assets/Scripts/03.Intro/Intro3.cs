using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Intro3 : MonoBehaviour {

    [SerializeField] private float Timer;
    [SerializeField] private float MovieTime;

    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Start()
    {

    }


    void Update()
    {
        Timer += Time.deltaTime;

        if (Timer > 66.0f)
            SceneManager.LoadScene("06.Stage1");

        if (Input.GetKey("escape"))
            SceneManager.LoadScene("06.Stage1");
    }
}
