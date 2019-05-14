//Author: Paschalis Tolios
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewInteractionListener : MonoBehaviour
{
    private GameObject objectToInteract;
    void Start()
    {
        EventSystem.Current.RegisterListener<InteractionEvent>(Interacted);
    }

    void Interacted(InteractionEvent info)
    {
        objectToInteract = info.interactedObject;

        if (objectToInteract.GetComponent<Interactable>() != null)
        {
            info.interactedObject.GetComponent<Interactable>().StartInteraction();

        }
    }
}

