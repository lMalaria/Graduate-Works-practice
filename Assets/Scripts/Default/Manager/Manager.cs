using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Manager : MonoBehaviour {

    private enum CanvasType
    {
        InGameUICanvas = 0,
        MenuCanvas,
        OptionsCanvas,
        StoryCanvas
    }

    [SerializeField]
    private Canvas[] canvases;

    private Camera[] allCameras;

    private bool isOnGaming;

    [SerializeField]
    private Toggle autoTargetMode;

    SWATPlayerController SWATPlayer;
    private GameObject SWATPlayerInstance;

    void Awake()  {
        allCameras = Camera.allCameras;

        isOnGaming = true;

        SWATPlayer = GetComponent<SWATPlayerController>();
        SWATPlayerInstance = GameObject.Find("SWAT");
    }

	void Start () {
        for (int i = 0; i < Camera.allCamerasCount; i++)
            print(allCameras[i]);

        SWATPlayer = SWATPlayerInstance.GetComponent<SWATPlayerController>();
    }
	
	void Update () {
        if (Input.GetKeyDown("f1"))
            allCameras[1].depth = 1;
        

        if (Input.GetKeyDown("f2"))
            allCameras[1].depth = -1;


        if (Input.GetKeyDown("escape") && isOnGaming == true)
        {
            isOnGaming = false;
            Cursor.visible = true;
            Time.timeScale = 0.0f;
            allCameras[0].depth = 0;
            canvases[(int)CanvasType.InGameUICanvas].enabled = false;
            canvases[(int)CanvasType.MenuCanvas].enabled = true;
        }
    }

    public void ResumeButton()
    {
        isOnGaming = true;
        Cursor.visible = false;
        Time.timeScale = 1.0f;
        allCameras[2].depth = -1;
        allCameras[0].depth = -2;
        canvases[(int)CanvasType.InGameUICanvas].enabled = true;
        canvases[(int)CanvasType.MenuCanvas].enabled = false;
    }

    public void OptionsButton()
    {
        canvases[(int)CanvasType.MenuCanvas].enabled = false;
        canvases[(int)CanvasType.OptionsCanvas].enabled = true;
    }

    public void MakeAimAutoTargetingModeToggle()
    {
        if(autoTargetMode.isOn)
            SWATPlayer.isAutoTargetingModeOn = true;
        //else(autoTargetMode.)
        else
            SWATPlayer.isAutoTargetingModeOn = false;

    }

    public void ReturnButton()
    {
        canvases[(int)CanvasType.MenuCanvas].enabled = true;
        canvases[(int)CanvasType.OptionsCanvas].enabled = false;
    }
}
