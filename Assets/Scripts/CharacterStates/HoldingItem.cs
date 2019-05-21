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
            soundEvent.gameObject = owner.gameObject;
            soundEvent.eventDescription = "PickUp Sound";
            soundEvent.audioClip = objectCarried.GetComponent<Interactable>().GetAudioClip();
            soundEvent.looped = false;
            if (soundEvent.audioClip != null)
            {
                EventSystem.Current.FireEvent(soundEvent);
            }
        }
    }
    public override void ExitState()
    {
        if (soundEvent != null)
        {

            StopSoundEvent stopSoundEvent = new StopSoundEvent();
            stopSoundEvent.AudioPlayer = soundEvent.objectPlaying;
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
        }



        if (objectCarried == null) {

            SetHolding(false);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            //owner.GetComponent<CharacterStateMachine>().environment = owner.GetComponent<CharacterStateMachine>().environment | (1 << layerNumber);

            ReleaseItem();
        }



        //Throw item

        if (Input.GetKeyDown(KeyCode.R))
        {
            Throw();
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
        died = false;
    }

    private void ReleaseItem()
    {
        objectCarried.layer = layerNumber;
        InteractWithObject();
        SetHolding(false);
    }
    private void TransformCarriedObject()
    {
        Vector3 target;
        target.x = 0;
        target.y = Camera.main.transform.eulerAngles.y;
        target.z = 0;
        objectCarried.transform.eulerAngles = target;
        objectCarried.transform.position = Direction();
        if (objectCarried.GetComponent<Rigidbody>() != null)
        {

            objectCarried.GetComponent<Rigidbody>().velocity = new Vector3(0, owner.GetComponent<CharacterStateMachine>().velocity.y, 0);
        }


    }

    Vector3 ThrowTo()
    {

        float x = LookDirection().x/2;
        float y = 10;
        float z = LookDirection().z/2;
        return new Vector3(x, y, z);

    }

    private void Throw()
    {
        objectCarried.layer = layerNumber;
        
        SetHolding(false);
        InteractWithObject();
        
        SetThrowEvent();
    }

    private void SetThrowEvent()
    {
        ThrowEvent throwInfo = new ThrowEvent();
        throwInfo.eventDescription = "Pressed item has been activated: ";
        throwInfo.gameObject = objectCarried;
        throwInfo.throwDirection = ThrowTo();

        EventSystem.Current.FireEvent(throwInfo);
    }


    private Vector3 Direction()
    {
        Vector3 project = Vector3.ProjectOnPlane(LookDirection(), Vector3.down).normalized;
        float x = owner.transform.position.x + project.x * (0.2f + capsuleCollider.radius + objectCarried.GetComponent<BoxCollider>().transform.localScale.x / 2);
        float y = objectCarried.transform.localScale.y / 2 + capsuleCollider.height / 2 + owner.transform.position.y;
        float z = owner.transform.position.z + project.z * (0.2f + capsuleCollider.radius + objectCarried.GetComponent<BoxCollider>().transform.localScale.z / 2);
        return new Vector3(x, y, z);
    }


}