using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrossHair : MonoBehaviour {

    [SerializeField]
    private float currentSpread;

    [SerializeField]
    private float speedOfSpread;

    [SerializeField]
    private Parts[] parts;

    [SerializeField]
    PlayerController playerController;

    [SerializeField]
    CharacterStatus characterStatus;

    private float t;

    private float curSpread;
	
    void Start()
    {
        GameObject.Find("Canvas").GetComponent<Canvas>().enabled = false;
    }

	void Update()
    {
        if (playerController.movementAmount > 0)
            currentSpread = 20 * (5 + playerController.movementAmount);
        else
            currentSpread = 20;

        if (characterStatus.isAiming == true)
            GameObject.Find("Canvas").GetComponent<Canvas>().enabled = true;
        else
            GameObject.Find("Canvas").GetComponent<Canvas>().enabled = false;

        UpdateCrossHair();
	}

    public void UpdateCrossHair()
    {
        t = 0.005f * speedOfSpread;
        curSpread = Mathf.Lerp(curSpread, currentSpread, t);

        for(int i = 0; i < parts.Length; i++)
        {
            Parts p = parts[i];

            p.rect.anchoredPosition = p.pos * curSpread;

        }
    }
}

[System.Serializable]
public class Parts
{
    public RectTransform rect;
    public Vector2 pos;
}