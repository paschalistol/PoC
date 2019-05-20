using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Typewriter : MonoBehaviour
{
    private string outputString = null;
    private bool done;
    private int i = 1;
     private Text displayedText;
    private char[] characters;
    private Text inputText;

    public void SetText(string text)
    {
        inputText.text = text;
    }

    void Update()
    {
        if (!done)
        {
            displayedText.text = Typewrite(inputText.text);
        }
    }

    private string Typewrite(string text)
    {
        i++;
        characters = text.ToCharArray();
        outputString = outputString + characters[i].ToString();
        if (i == (characters.Length -1))
        {
            done = true;
        }
        return outputString;
    }
    
}
