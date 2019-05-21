//Author: Paschalis Tolios

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestSystem : Interactable
{
    [SerializeField] private GameObject questPanel;
    [SerializeField] private Text questText;
    [TextArea]
    [SerializeField] private string textToDisplay;
    [SerializeField] private float timeToShowQuestText;
    [SerializeField] private float timeShown;
    public override AudioClip GetAudioClip()
    {
        throw new System.NotImplementedException();
    }

    public override void StartInteraction()
    {

        questText.text = textToDisplay;
        questPanel.SetActive(true);
        //timeShown = 0;
        //StartCoroutine(CloseQuestText());
    }

    IEnumerator  CloseQuestText()
    {
        while (timeToShowQuestText > timeShown)
        {
            timeShown += Time.deltaTime;
            yield return null;
        }
        questPanel.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            questPanel.SetActive(true);
        }else if (Input.GetKeyUp(KeyCode.Tab))
        {
            questPanel.SetActive(false);
        }
    }

}
