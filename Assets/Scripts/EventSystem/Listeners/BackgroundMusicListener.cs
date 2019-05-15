using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BackgroundMusicListener : MonoBehaviour
{
    private int numberOfEnemiesChasing;
    [SerializeField] private AudioClip clipWhenChased, clipWhenNotChased;
    private AudioSource audioSource;
    private int numberOfEnemiesBeforeChange;
    void Start()
    {
        EventSystem.Current.RegisterListener<MusicBasedOnChased>(ChangeEnemiesChasing);
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = clipWhenNotChased;
        audioSource.Play();
    }


    void ChangeEnemiesChasing(MusicBasedOnChased musicBasedOnChased)
    {
        numberOfEnemiesBeforeChange = numberOfEnemiesChasing;
        numberOfEnemiesChasing = musicBasedOnChased.enemyChasing ? ++numberOfEnemiesChasing : --numberOfEnemiesChasing;

        if (numberOfEnemiesBeforeChange == 0 && numberOfEnemiesChasing > 0)
        {
            audioSource.clip = clipWhenChased;
            audioSource.Play();
        }
        else if (numberOfEnemiesBeforeChange > 0 && numberOfEnemiesChasing == 0)
        {
            audioSource.clip = clipWhenNotChased;
            audioSource.Play();
        }

        

    }

}
