using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class DayToNightCycle : MonoBehaviour {

    [SerializeField]
    private float time;

    [SerializeField]
    private TimeSpan currentTime;

    [SerializeField]
    private Transform sunTransform;

    [SerializeField]
    private Light sun;

    [SerializeField]
    private Text timeText;

    [SerializeField]
    private int days;

    [SerializeField]
    private float intensity;

    [SerializeField]
    private Color fogDay = Color.gray;

    [SerializeField]
    private Color fogNight = Color.black;

    private int speed;

    private bool isNight;

    void Start ()
    {
        isNight = true;
        time = 0;
		speed = 24;
	}
	
	void Update ()
    {
        ChangeTime();
	}

    void ChangeTime()
    {
        time += Time.deltaTime * 1000;

        if (time > 86400)
        {
            days += 1;
            time = 0;
        }

        currentTime = TimeSpan.FromSeconds(time);
        string[] tempTime = currentTime.ToString().Split(":"[0]);

        timeText.text = tempTime[0] + ":" + tempTime[1];

        sunTransform.rotation = Quaternion.Euler(new Vector3((time - 21600) / 86400 * 360, 0, 0));

        if (time < 43200)
            intensity = 1 - (43200 - time) / 43200;
        else
            intensity = 1 - ((43200 - time) / 43200 * -1);

        RenderSettings.fogColor = Color.Lerp(fogNight, fogDay, intensity * intensity);

        sun.intensity = intensity;
    }
}
