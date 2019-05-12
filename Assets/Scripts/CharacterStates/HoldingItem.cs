//Main Author: Paschalis Tolios
//Secondary Author: Emil Dahl

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Character/HoldingItem")]
public class HoldingItem : HoldItemBase
{
    private int layerNumber;
    private SoundEvent soundEvent;
    public override void EnterState()
    {

        base.EnterState();
        objectCarried = ReturnObjectInFront();
        if (objectCarried != null)
        {
            layerNumber = objectCarried.layer;
            if (layerNumber != 17)
            {
                objectCarried.layer = 0;
            }
            else
            {
                objectCarried.transform.parent = null;
            }
            soundEvent = new SoundEvent();
            soundEvent.gameObject = owner.gameObject;
            soundEvent.eventDescription = "PickUp Sound";
            soundEvent.audioClip = objectCarried.GetComponent<Interaction>().GetAudioClip();
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


        if (objectCarried == null)
            SetHolding(false);

        if (Input.GetKeyDown(KeyCode.E))
        {
            //owner.GetComponent<CharacterStateMachine>().environment = owner.GetComponent<CharacterStateMachine>().environment | (1 << layerNumber);
            objectCarried.layer = layerNumber;
            InteractWithObject();
            SetHolding(false);
        }



        //Throw item

        if (Input.GetKeyDown(KeyCode.R))
        {
            objectCarried.layer = layerNumber;
            objectCarried.transform.position += ThrowTo();
            objectCarried.GetComponent<Rigidbody>().AddForce(ThrowTo() * 3);
            SetHolding(false);
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

        float x = LookDirection().x * 3;
        float y = 50;
        float z = LookDirection().z * 3;
        return new Vector3(x, y, z);

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