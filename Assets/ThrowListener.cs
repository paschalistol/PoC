using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowListener : MonoBehaviour
{

    private GameObject objectToInteract;
    void Start()
    {
        EventSystem.Current.RegisterListener<ThrowEvent>(Throw);
    }

    
    void Throw(ThrowEvent eventInfo)
    {
        objectToInteract = eventInfo.gameObject;
        Interactable interact = objectToInteract.GetComponent<Interactable>();
        if (interact != null)
        {
            
            interact.BeingThrown(eventInfo.throwDirection);
        }
    }
}
