using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayVideo : MonoBehaviour {


    [SerializeField] private MovieTexture Movie;
    [SerializeField] private RawImage Image;
    [SerializeField] private AudioSource Audio;


    void Awake()
    {
        Image = GetComponent<RawImage>();
        Audio = GetComponent<AudioSource>();
    }
	// Use this for initialization
	void Start ()
    {
        Play();
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void Play()
    {
        Image.texture = Movie;
        Movie.Play();
        Audio.clip = Movie.audioClip;
        Audio.Play();
    }
}
