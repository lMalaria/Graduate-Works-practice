using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Intro4 : MonoBehaviour {

    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Start()
    {
        Invoke("changeToFirstStage", 40);
    }

    void Update()
    {
        if (Input.GetKey("escape"))
        {
            SceneManager.LoadScene("07.Stage1");
        }
    }

    private void changeToFirstStage()
    {
        SceneManager.LoadScene("07.Stage1");
    }
}
