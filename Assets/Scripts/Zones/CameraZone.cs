//Main Author: Emil Dahl
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZone : Interactable
{
    protected override void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        GameController.activatedAlarm = true;
    }

    public override void StartInteraction()
    {
        Destroy(gameObject);
    }

    public override AudioClip GetAudioClip()
    {
        throw new System.NotImplementedException();
    }

    private void SwitchAlarm()
    {
        //if (!GameController.disabledAlaram)
        //    GameController.activatedAlarm = true;
    }

   
}
