﻿//Author: Paschalis Tolios
//Secondary Authors: Emil Dahl, Johan Ekman
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    [SerializeField] private AudioClip pickupSound;
    public AudioClip GetAudioClip()
    {
        return pickupSound;
    }
    public void StartInteraction(GameObject gameObject)
    {
        switch (gameObject.tag)
        {
            //case 11:


            //    gameObject.GetComponent<Boxes2>().InteractWithBox();

            //break;
            //case 15:
            ////    //bool hasKey = gameObject.GetComponent<Key>().used;
            ////gameObject.GetComponent<Door>().InteractWithDoor();

            //break;
            //case "Battery":
            //   gameObject.GetComponent<Battery>().PickUpBattery();
            //  break;
            //case 17:
            //    //gameObject.GetComponent<Key>().TakeKeyInteraction();
            //    break;
            //case "SafeDoor":
            //    gameObject.GetComponent<Door>().
            //    break;
            case "SceneTransfer":
                gameObject.GetComponent<SceneTransfer>().ChangeLevel();
                break;
            case "Key":
                gameObject.GetComponent<Key>().KeyInteraction();
                break;
            case "Box":
                gameObject.GetComponent<Boxes>().BoxInteraction();
                break;
            case "FuseBoxItem":
                gameObject.GetComponent<FuseBoxItem>().isHeld = !gameObject.GetComponent<FuseBoxItem>().isHeld;
                break;
            case "Gold":
                gameObject.GetComponent<Gold>().isHeld = !gameObject.GetComponent<Gold>().isHeld;
                break;
            case "Battery":
                gameObject.GetComponent<Battery>().isHeld = !gameObject.GetComponent<Battery>().isHeld;
                break;
            default:
                break;
        }
    }
}
