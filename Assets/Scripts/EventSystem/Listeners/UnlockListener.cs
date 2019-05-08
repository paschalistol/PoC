//Main Author: Emil Dahl

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockListener : MonoBehaviour
{
    private GameObject objectToInteract;
    void Start()
    {
        EventSystem.Current.RegisterListener<UnlockEvent>(UnlockDoorInteraction);
    }

    //sorry but it has to be done

    void UnlockDoorInteraction(UnlockEvent doorInfo)
    {
        objectToInteract = doorInfo.doorObject;
        objectToInteract.GetComponent<Door>().InteractWithDoor();
    }
}
