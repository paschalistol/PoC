//Author: Paschalis Tolios

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldItemBase : State
{
    protected CharacterHoldItemStateMachine owner;

    public GameObject objectCarried;
    protected CapsuleCollider capsuleCollider;
    public bool HoldingSth {get { return owner.holdingSth; }set { owner.holdingSth = value; } }
    private Vector3 point2, point1;
    private RaycastHit raycastHit;
    private float pickupCoeff = 2;
    public override void InitializeState(StateMachine owner)
    {
        this.owner = (CharacterHoldItemStateMachine)owner;
    }

    public override void EnterState()
    {
        capsuleCollider = owner.GetComponent<CapsuleCollider>();

        point1 = capsuleCollider.center + Vector3.up * (capsuleCollider.height / 2 - capsuleCollider.radius);
        point2 = capsuleCollider.center + Vector3.down * (capsuleCollider.height / 2 - capsuleCollider.radius);
    }

    protected void InteractWithObject()
    {

        if (objectCarried != null)
        {

            InteractionEvent interactedInfo = new InteractionEvent();
            interactedInfo.eventDescription = "Pressed item has been activated: ";
            interactedInfo.interactedObject = objectCarried;

            EventSystem.Current.FireEvent(interactedInfo);
            owner.objectHolding = objectCarried;
        }

    }

    protected GameObject ReturnObjectInFront()
    {


        Physics.CapsuleCast(owner.transform.position + point1, owner.transform.position + point2, capsuleCollider.radius, owner.transform.forward, out raycastHit, capsuleCollider.radius*pickupCoeff, owner.Interactables);

        if (raycastHit.collider != null)
        {
            objectCarried = raycastHit.transform.gameObject;

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

