using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldItemBase : State
{
    protected CharacterHoldItemStateMachine owner;

    protected GameObject objectCarried;
    protected CapsuleCollider capsuleCollider;
    protected bool HoldingSth {get { return owner.holdingSth; }set { owner.holdingSth = value; } }


    public override void InitializeState(StateMachine owner)
    {
        this.owner = (CharacterHoldItemStateMachine)owner;
    }

    public override void EnterState()
    {
        capsuleCollider = owner.GetComponent<CapsuleCollider>();
    }

    protected void InteractWithObject()
    {


            GameObject gameObject = objectCarried;
            Debug.Log(gameObject.tag);
            if (gameObject != null)
            {

                InteractionEvent interactedInfo = new InteractionEvent();
                interactedInfo.eventDescription = "Pressed item has been activated: ";
                interactedInfo.interactedObject = gameObject;


                EventSystem.Current.FireEvent(interactedInfo);
            }
        
    }

    protected GameObject ReturnObjectInFront()
    {
        Vector3 look = LookDirection();
        Vector3 point1 = owner.transform.position + capsuleCollider.center + Vector3.up * (capsuleCollider.height / 2 - capsuleCollider.radius);
        Vector3 point2 = owner.transform.position + capsuleCollider.center + Vector3.down * (capsuleCollider.height / 2 - capsuleCollider.radius);
        RaycastHit raycastHit;
        bool capsuleCast = Physics.CapsuleCast(point1, point2, capsuleCollider.radius, LookDirection().normalized, out raycastHit, capsuleCollider.radius, owner.pickups);

        if (raycastHit.collider != null)
        {
            objectCarried = raycastHit.transform.gameObject;
            objectCarried.transform.position += new Vector3(0, 0.5f, 0);
            return objectCarried;
        }
        return null;
    }
    protected void SetHolding(bool holds)
    {
        HoldingSth = holds;
    }
    

    protected Vector3 LookDirection()
    {
        return -Camera.main.GetComponent<CameraScript>().getRelationship();
    }
}

