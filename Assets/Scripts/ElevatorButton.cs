//Author: Paschalis Tolios
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorButton : Interactable
{
    [SerializeField] private Lift2 lift;
    public override AudioClip GetAudioClip()
    {
        return null;
    }

    public override void StartInteraction()
    {
        lift.ActivateLift();
    }

}
