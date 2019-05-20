//Author: Paschalis Tolios
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionListener : MonoBehaviour
{
    private GameObject objectToInteract;
    void Start()
    {
        EventSystem.Current.RegisterListener<InteractionEvent>(Interacted);
    }

    void Interacted(InteractionEvent info)
    {
        objectToInteract = info.interactedObject;
        Interactable interact = objectToInteract.GetComponent<Interactable>();


        if (interact != null)
        {
            interact.StartInteraction();
            
        }
    }
}

