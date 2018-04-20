using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeOut : MonoBehaviour {

    [SerializeField]
    private Image fadeScreen;

    private Color screenAlpha;

    private float transparence;

    private float fadeSpeed;

	void Start ()
    {
        fadeScreen = GetComponent<Image>();

        var startColor = fadeScreen.color;
        startColor.a = 0.0f;
        fadeScreen.color = startColor;

        fadeSpeed = 0.004f;
	}
	

	void Update ()
    {
        //print(screenAlpha);
        // needColor.a -= fadeSpeed; 
	}

    void Fadeout()
    {
        screenAlpha = fadeScreen.color;
        screenAlpha.a -= fadeSpeed;
        fadeScreen.color = screenAlpha;
    }

    void Fadein()
    {
        screenAlpha = fadeScreen.color;
        screenAlpha.a += fadeSpeed;
        fadeScreen.color = screenAlpha;
    }

    //void FadeinEffect()
    //{
    //    transparence = Mathf.Clamp(transparence, 1, 0);
    //    transparence -= fadeSpeed;
    //}

    //void FadeoutEffect()
    //{
    //    transparence = Mathf.Clamp(transparence, 0, 1);
    //    transparence += fadeSpeed;
    //}
}
