using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButton : MonoBehaviour {

    [SerializeField] private float Timer;

	
    void Awake()
    {
        Cursor.visible = true;
    }

	void Start ()
    {

	}
	
	
	void Update ()
    {
		
	}

    public void EditorModeButton()
    {
        SceneManager.LoadScene("02.Editor Mode");
    }

    public void StartButton()
    {
        SceneManager.LoadScene("03.Intro1");
    }

    public void ExitButton()
    {
        Debug.Log("Game has been quit");
        Application.Quit();
    }


}
