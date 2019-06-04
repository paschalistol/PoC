//Author: Paschalis Tolios
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorButton : Interactable
{
    [SerializeField] private Lift2 lift;
    [SerializeField] private AudioClip activationSound;
    private SoundEvent soundEvent;
    public override AudioClip GetAudioClip()
    {
        return activationSound;
    }

    public override void StartInteraction()
    {
        lift.ActivateLift();
        soundEvent = new SoundEvent();

        soundEvent.eventDescription = "Elevator Sound";
        soundEvent.audioClip = activationSound;
        soundEvent.looped = false;
        if (soundEvent.audioClip != null)
        {
            EventSystem.Current.FireEvent(soundEvent);
        }
    }

}
