//Author: Paschalis Tolios

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransfer : Interactable
{
    public int levelToLoad;

    [SerializeField]
    private AudioClip changeSceneSound;

    [SerializeField]
    //private int levelToLoad = 1;
    private SoundEvent soundEvent;
    public override AudioClip GetAudioClip()
    {
        return changeSceneSound;
    }

    public override void StartInteraction()
    {
        soundEvent = new SoundEvent
        {
            eventDescription = "PickUp Sound",
            audioClip = changeSceneSound,
            looped = false
        };
        if (soundEvent.audioClip != null)
        {
            EventSystem.Current.FireEvent(soundEvent);
            StartCoroutine(WaitForSound());
        }
        SceneManager.LoadScene(levelToLoad);
    }
    IEnumerator  WaitForSound()
    {
        while (soundEvent.objectPlaying != null)
        {

        yield return null;
        }
    }
}
