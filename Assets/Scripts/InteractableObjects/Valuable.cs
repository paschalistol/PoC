//Main Author: Johan Ekman
//Secondary Author: Paschalis Tolios

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Valuable : Interactable
{

    public float value;
    protected const float skinWidth = 0.2f;
    [SerializeField] private LayerMask environment;
    [SerializeField] private AudioClip interactionSound = null;
    private AddPointEvent addPointInfo;
    private SoundEvent soundEvent;

    protected override void Start()
    {
        base.Start();  
    
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartInteraction();
        }
    }

    protected virtual void Update()
    {
        AddPhysics();
        transform.position += Velocity * Time.deltaTime;
    }

    private void AddPhysics()
    {
        if (!isHeld)
        {

            base.Velocity = PhysicsScript.Decelerate(base.Velocity);
            base.Velocity = PhysicsScript.Gravity(base.Velocity);
        }
        else
        {
            GetWallNormal();
        }
        base.Velocity = PhysicsScript.CollisionCheck(base.Velocity, boxCollider, skinWidth, environment);

    }
    public override void StartInteraction()
    {
        base.StartInteraction();
        addPointInfo = new AddPointEvent();
        addPointInfo.eventDescription = "Getting points!";
        addPointInfo.point = value;
        EventSystem.Current.FireEvent(addPointInfo);
        SoundEvent soundEvent = new SoundEvent();

        soundEvent.eventDescription = "Jump Sound";
        soundEvent.audioClip = interactionSound;
        soundEvent.looped = false;
        if (soundEvent.audioClip != null)
        {
            EventSystem.Current.FireEvent(soundEvent);
        }
        Destroy(gameObject);
    }

    public override AudioClip GetAudioClip()
    {
        return interactionSound;
    }
}
