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
    private GameObject objectInFront;
    public override void EnterState()
    {

        base.EnterState();
        
        //owner.objectCarried = ReturnObjectInFront();
        if (owner.objectCarried != null)
        {
            layerNumber = owner.objectCarried.layer;
            owner.objectCarried.layer = 0;
            owner.objectCarried.transform.parent = null;

            soundEvent = new SoundEvent();

            soundEvent.eventDescription = "PickUp Sound";
            soundEvent.audioClip = owner.objectCarried.GetComponent<Interactable>().GetAudioClip();
            soundEvent.looped = false;
            if (soundEvent.audioClip != null)
            {
                EventSystem.Current.FireEvent(soundEvent);
            }
            owner.objectCarried.transform.position = new Vector3(owner.objectCarried.transform.position.x, owner.objectCarried.transform.localScale.y / 2 + capsuleCollider.height * 0.25f + owner.transform.position.y, owner.objectCarried.transform.position.z);
            owner.objectCarried.transform.rotation = owner.transform.rotation;
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

        //objectInFront = ReturnObjectInFront();
        //if (objectInFront == null && owner.objectCarried != null)
        //{
        //    Debug.Log("test");
        //    ReleaseItem();
        //}


       /* if (owner.objectCarried == null || !owner.objectCarried.GetComponent<Interactable>().IsHeld())
        {

            SetHolding(false);
        }*/

        if (Input.GetKeyDown(KeyCode.E))
        {


            ReleaseItem();
            
        }

        

        //Throw item

        if (Input.GetKeyDown(KeyCode.R))
        {
            Throw();
            owner.objectCarried = null;
        }
        if (!HoldingSth)
        {
            owner.ChangeState<EmptyHands>();
        }
        if (owner.objectCarried != null)
        {
            TransformCarriedObject();
        }
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

        if (owner.objectCarried != null)
        {

            GameObject temp = owner.objectCarried;
            ReleaseItem();
            RespawnItem(temp);
        }
    }

    private void ReleaseItem()
    {
        owner.objectCarried.layer = layerNumber;
        InteractWithObject();
        owner.objectCarried = null;
        SetHolding(false);
    }
    private void TransformCarriedObject()
    {

        owner.objectCarried.GetComponent<Interactable>().RotateAround(owner.transform);

        owner.objectCarried.GetComponent<Interactable>().SetVelocity(owner.GetComponent<CharacterStateMachine>().velocity);

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
        owner.objectCarried.layer = layerNumber;

        SetHolding(false);
        SetThrowEvent();
        InteractWithObject();

    }

    private void SetThrowEvent()
    {
        ThrowEvent throwInfo = new ThrowEvent();
        throwInfo.eventDescription = "Pressed item has been activated: ";
        throwInfo.gameObject = owner.objectCarried;
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
        rotateX = owner.transform.position.x + project.x * (0.2f + capsuleCollider.radius + owner.objectCarried.GetComponent<BoxCollider>().transform.localScale.x / 2);
        rotateY = owner.objectCarried.transform.localScale.y / 2 + capsuleCollider.height / 2 + owner.transform.position.y;
        rotateZ = owner.transform.position.z + project.z * (0.2f + capsuleCollider.radius + owner.objectCarried.GetComponent<BoxCollider>().transform.localScale.z / 2);
        owner.objectCarried.transform.position = new Vector3(rotateX, rotateY, rotateZ);
    }

}
    private Vector3 Direction()
    {
        project = Vector3.ProjectOnPlane(LookDirection(), Vector3.down).normalized;
        float x = owner.transform.position.x + project.x * (0.2f + capsuleCollider.radius + owner.objectCarried.GetComponent<BoxCollider>().transform.localScale.x / 2);
        float y = owner.objectCarried.transform.localScale.y / 2 + capsuleCollider.height / 2 + owner.transform.position.y;
        float z = owner.transform.position.z + project.z * (0.2f + capsuleCollider.radius + owner.objectCarried.GetComponent<BoxCollider>().transform.localScale.z / 2);
        return new Vector3(x, y, z);
    }
    Vector3 project;
*/
#endregion