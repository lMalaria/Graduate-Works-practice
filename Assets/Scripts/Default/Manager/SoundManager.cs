using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    public static SoundManager Instance = null;

    void Awake()
    {
        if (Instance == null)
            Instance = this;

        else
        {
            //Destroy this, this enforces our singleton pattern so there can only be one instance of SoundManager.
            Destroy(gameObject);
        }

        //Set SoundManager to DontDestroyOnLoad so that it won't be destroyed when reloading our scene.
        DontDestroyOnLoad(gameObject);
    }

	void Start ()
    {

    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void PlayBGM(AudioSource bgmAudioSource, AudioClip bgmClip, bool isLoop, float soundVolume)
    {
        bgmAudioSource.clip = bgmClip;
        bgmAudioSource.loop = isLoop;
        bgmAudioSource.volume = soundVolume;
        bgmAudioSource.Play();
    }

    public void PlaySound(AudioClip clip, GameObject objectToPlayOn)
    {
        //AudioSource.PlayClipAtPoint(clip, objectToPlayOn.transform.position);
        AudioSource.PlayClipAtPoint(clip, objectToPlayOn.transform.position);
    }
}
