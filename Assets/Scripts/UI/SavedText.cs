using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SavedText : MonoBehaviour
{
    Text text;
    void Start()
    {
        text = GetComponent<Text>();
        text.enabled = false;
    }

    public void ShowText()
    {
        StartCoroutine(ShowingText());
    }

    IEnumerator ShowingText()
    {

        text.enabled = true;
        yield return new WaitForSeconds(2);
        text.enabled = false;
    }
}
