using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScript : MonoBehaviour {

    [SerializeField]
    private AudioSource bgmSource;

    [SerializeField]
    private AudioClip bgmClip;

	void Start ()
    {
        PlayBGM();
	}
	
	void Update ()
    {

	}
    
    void PlayBGM()
    {
        SoundManager.Instance.PlayBGM(bgmSource, bgmClip, true, 0.4f);
    }

    void TitleScene()
    {

    }
}
