//Main Author: Emil Dahl

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryZone : MonoBehaviour
{
    [SerializeField] private AudioClip victorySound;
    private SoundEvent sound;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(0);
            if (sound != null)
            {
                sound.audioClip = victorySound;
                sound.eventDescription = "Victory Sound";
                sound.objectInstatiated = gameObject;

                EventSystem.Current.FireEvent(sound);
            }
        }


    }
}
