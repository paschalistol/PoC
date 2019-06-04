//Main Author: Emil Dahl
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuseBox : MonoBehaviour
{
    [SerializeField] private GameObject[] interactionObject;
    [SerializeField] private int itemQuantity = 2;
    [HideInInspector] public int count = 0;
    [HideInInspector] public bool itemUsed;
    [SerializeField] private string activationText = "";
    private WarningEvent activationEvent;
    private void Start()
    {
        activationEvent = new WarningEvent();
    }
    public void RunInteraction()
    {
        count++;
        if (activationText != "")
        {

            activationEvent.warning = activationText;
            EventSystem.Current.FireEvent(activationEvent);
        }
        if (count >= itemQuantity)
            foreach(GameObject ob in interactionObject) { ob.GetComponent<Interactable>().StartInteraction(); }
            
    }
}

#region legacy
//Run particles on fusebox activation of door
//OpenDoorEvent doorEvent = new OpenDoorEvent();
//doorEvent.gameObject = gameObject;
//doorEvent.eventDescription = "A door has been opened!";
//doorEvent.particles = endParticles;

//EventSystem.Current.FireEvent(doorEvent);
#endregion