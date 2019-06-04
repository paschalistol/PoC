//Main Author: Paschalis Tolios
//Secondary Author: Emil Dahl Johan Ekman

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Character/HoldingItem")]
public class HoldingItem : HoldItemBase
{
    private int layerNumber;
    private SoundEvent soundEvent;
    private static bool died;
    private float holdItemOffset = 1f;
    public override void EnterState()
    {

        base.EnterState();
        objectCarried = ReturnObjectInFront();
        if (objectCarried != null)
        {
            layerNumber = objectCarried.layer;
            objectCarried.layer = 0;
            objectCarried.transform.parent = null;

            soundEvent = new SoundEvent();

            soundEvent.eventDescription = "PickUp Sound";
            soundEvent.audioClip = objectCarried.GetComponent<Interactable>().GetAudioClip();
            soundEvent.looped = false;
            if (soundEvent.audioClip != null)
            {
                EventSystem.Current.FireEvent(soundEvent);
            }
            objectCarried.transform.position = new Vector3(objectCarried.transform.position.x, objectCarried.transform.localScale.y/2 + capsuleCollider.height/2 + owner.transform.position.y, objectCarried.transform.position.z);
            objectCarried.transform.rotation = owner.transform.rotation;
        }
    }
    public override void ExitState()
    {
        if (soundEvent != null)
        {

            StopSoundEvent stopSoundEvent = new StopSoundEvent();
            stopSoundEvent.AudioPlayer = soundEvent.objectInstatiated;
            stopSoundEvent.eventDescription = "Stop Sound";
            if (stopSoundEvent.AudioPlayer != null)
            {
                EventSystem.Current.FireEvent(stopSoundEvent);
            }
        }

        base.ExitState();
    }

    public override void ToDo()
    {

        if (died)
        {
            ReleaseAndRespawn();
            died = false;
        }

        if (objectCarried != null)
        {
            lhs = new Vector2(owner.transform.position.x, owner.transform.position.z);
            rhs = new Vector2(objectCarried.transform.position.x, objectCarried.transform.position.z);
            if ((Vector2.Distance(lhs, rhs) > Mathf.Max(objectCarried.transform.localScale.x + capsuleCollider.radius + holdItemOffset, objectCarried.transform.localScale.z + capsuleCollider.radius + holdItemOffset) || ObjectStillInFront() == false))
            {
                ReleaseItem();
            }
        }

        if (objectCarried == null || !objectCarried.GetComponent<Interactable>().IsHeld())
        {

            SetHolding(false);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {


            ReleaseItem();
        }



        //Throw item

        if (Input.GetKeyDown(KeyCode.R))
        {
            Throw();
            objectCarried = null;
        }
        if (!HoldingSth)
        {
            owner.ChangeState<EmptyHands>();
        }
        if (objectCarried != null)
        {
            TransformCarriedObject();
        }
    }
    Vector2 lhs, rhs;
    private bool ObjectStillInFront()
    {
        lhs = new Vector2(LookDirection().x, LookDirection().z);
        rhs = new Vector2(objectCarried.transform.position.x- owner.transform.position.x, objectCarried.transform.position.z- owner.transform.position.z);
       return  Vector2.Dot(lhs.normalized, rhs.normalized) > 0;
    }
    private void RespawnItem(GameObject temp)
    {

        if (temp != null)
        {

            temp.GetComponent<Interactable>().RespawnItem();

        }
    }
    public static void SetDied()
    {
        died = true;
    }
    public void ReleaseAndRespawn()
    {

        if (objectCarried != null)
        {

            GameObject temp = objectCarried;
            ReleaseItem();
            objectCarried = null;
            RespawnItem(temp);
        }
    }

    private void ReleaseItem()
    {
        objectCarried.layer = layerNumber;
        InteractWithObject();
        SetHolding(false);
    }
    private void TransformCarriedObject()
    {

        objectCarried.GetComponent<Interactable>().RotateAround(owner.transform);

        objectCarried.GetComponent<Interactable>().SetVelocity(owner.GetComponent<CharacterStateMachine>().velocity);

    }
    Vector3 xyz;
    Vector3 ThrowTo()
    {

        xyz.x = LookDirection().x * 1.2f;
        xyz.y = 20;
        xyz.z = LookDirection().z * 1.2f;
        return xyz;

    }

    private void Throw()
    {
        objectCarried.layer = layerNumber;

        SetHolding(false);
        SetThrowEvent();
        InteractWithObject();

    }

    private void SetThrowEvent()
    {
        ThrowEvent throwInfo = new ThrowEvent();
        throwInfo.eventDescription = "Pressed item has been activated: ";
        throwInfo.gameObject = objectCarried;
        throwInfo.throwDirection = ThrowTo();

        EventSystem.Current.FireEvent(throwInfo);
    }


}

#region Legacy
/*
 * 
 * 
 * 
            //owner.GetComponent<CharacterStateMachine>().environment = owner.GetComponent<CharacterStateMachine>().environment | (1 << layerNumber);
float rotateX, rotateY, rotateZ;
private void RotateAroundPlayer()
{
    project = Vector3.ProjectOnPlane(LookDirection(), Vector3.down).normalized;
    if (Mathf.Abs(Input.GetAxis("Mouse X")) > 0)
    {
        rotateX = owner.transform.position.x + project.x * (0.2f + capsuleCollider.radius + objectCarried.GetComponent<BoxCollider>().transform.localScale.x / 2);
        rotateY = objectCarried.transform.localScale.y / 2 + capsuleCollider.height / 2 + owner.transform.position.y;
        rotateZ = owner.transform.position.z + project.z * (0.2f + capsuleCollider.radius + objectCarried.GetComponent<BoxCollider>().transform.localScale.z / 2);
        objectCarried.transform.position = new Vector3(rotateX, rotateY, rotateZ);
    }
}
    private Vector3 Direction()
    {
        project = Vector3.ProjectOnPlane(LookDirection(), Vector3.down).normalized;
        float x = owner.transform.position.x + project.x * (0.2f + capsuleCollider.radius + objectCarried.GetComponent<BoxCollider>().transform.localScale.x / 2);
        float y = objectCarried.transform.localScale.y / 2 + capsuleCollider.height / 2 + owner.transform.position.y;
        float z = owner.transform.position.z + project.z * (0.2f + capsuleCollider.radius + objectCarried.GetComponent<BoxCollider>().transform.localScale.z / 2);
        return new Vector3(x, y, z);
    }
    Vector3 project;
*/
#endregion