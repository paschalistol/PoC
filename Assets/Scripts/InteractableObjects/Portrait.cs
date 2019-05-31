//Main Author: Emil Dahl
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portrait : Interactable
{
    private AddPointEvent points;
    private SoundEvent sound;
    [SerializeField] private AudioClip clip;
    [SerializeField] private float value = 1000;
    [SerializeField] private GameObject victoryZone;

    public override AudioClip GetAudioClip()
    {
        throw new System.NotImplementedException();
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            StartInteraction();
    }

    public override void StartInteraction()
    {
        if(victoryZone != null)
        victoryZone.SetActive(true);

        points = new AddPointEvent();
        points.eventDescription = "Getting points!";
        points.point = value;
        EventSystem.Current.FireEvent(points);

        sound = new SoundEvent();

        sound.eventDescription = "MonaLisa Sound";
        sound.audioClip = clip;
        sound.looped = false;
        if (sound.audioClip != null)
            EventSystem.Current.FireEvent(sound);
        
        Destroy(gameObject);
    }
}
