using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Intro3 : MonoBehaviour {

    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Start()
    {
        Invoke("changeToFourthIntro", 80);
    }

    void Update()
    {
        if (Input.GetKey("escape"))
        {
            Cursor.visible = true;
            SceneManager.LoadScene("06.Intro4");
        }
    }

    private void changeToFourthIntro()
    {
        SceneManager.LoadScene("06.Intro4");
    }
}
