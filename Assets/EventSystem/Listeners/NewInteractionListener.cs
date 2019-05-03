// Paschalis Tolios
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

        objectToInteract.GetComponent<Interaction>().StartInteraction(objectToInteract);
    }
}

