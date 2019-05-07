//Main Author: Paschalis Tolios
//Secondary author: Emil Dahl
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : MonoBehaviour
{
    
    public GameObject fuseBox;
    public GameObject lift;
    private bool used = false;
 

    private void Update()
    {
        RaycastHit raycastHit;
        bool boxCast = Physics.BoxCast(transform.position, transform.localScale, Vector3.down, out raycastHit, transform.rotation, transform.localScale.y + 0.003f);
        if (raycastHit.collider != null && raycastHit.collider.transform.gameObject == fuseBox)
        {
           
            InteractionEvent interactedInfo = new InteractionEvent();
            interactedInfo.eventDescription = "Pressed item has been activated: ";
            interactedInfo.interactedObject = gameObject;

            lift.GetComponent<Lift2>().onOff = true;
            EventSystem.Current.FireEvent(interactedInfo);
            Destroy(gameObject);
            // used = true;
        }
    }


}
