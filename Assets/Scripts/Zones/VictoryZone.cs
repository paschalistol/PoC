//Main Author: Emil Dahl

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// This class allows for the player to end the game
/// </summary>
public class VictoryZone : MonoBehaviour
{
    [SerializeField] private AudioClip victorySound;
    private SoundEvent sound;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (sound != null)
            {
                sound.audioClip = victorySound;
                sound.eventDescription = "Victory Sound";
                sound.objectInstatiated = gameObject;

                EventSystem.Current.FireEvent(sound);
            }
            SceneManager.LoadScene(0);
        }


    }
}
