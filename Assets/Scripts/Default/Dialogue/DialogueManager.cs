using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {

    [SerializeField]
    private GameObject textBox;

    [SerializeField]
    private Text theText;

    [SerializeField]
    private TextAsset textFile;

    [SerializeField]
    private string[] textLines;

    private int currentLine;
    private int endAtLine;

	void Start ()
    {
        currentLine = 0;

		if(textFile != null)
        {
            textLines = (textFile.text.Split('\n'));
        }

        if(endAtLine == 0)
        {
            endAtLine = textLines.Length - 1;
        }
	}

    void Update()
    {
        theText.text = textLines[currentLine];

        if(Input.GetKeyDown(KeyCode.Return))
        {
            currentLine += 1;
        }

        if(currentLine > endAtLine)
        {
            DisableTextBox();
        }
    }

    void DisableTextBox()
    {
        textBox.SetActive(false);
    }

    public void ResetText(TextAsset text)
    {
        if(theText != null)
        {
            textLines = new string[1];
            textLines = (textFile.text.Split('n'));
        }
    }
	
}
