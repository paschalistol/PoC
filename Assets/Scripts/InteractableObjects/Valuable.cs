//Main Author: Johan Ekman
//Secondary Author: Paschalis Tolios

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Valuable : Interactable
{

    public float value;
    protected Vector3 velocity;
    protected BoxCollider boxCollider;
    protected const float skinWidth = 0.2f;
    [SerializeField] private LayerMask environment;
    [SerializeField] private AudioClip interactionSound = null;
    private AddPointEvent addPointInfo;
    private SoundEvent soundEvent;

    protected override void Start()
    {
        base.Start();
        boxCollider = GetComponent<BoxCollider>();
        
        

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartInteraction();
        }
    }

    protected virtual void Update()
    {
        
            velocity = PhysicsScript.Decelerate(velocity);
            velocity = PhysicsScript.Gravity(velocity);
            velocity = PhysicsScript.CollisionCheck(velocity, boxCollider, skinWidth, environment);
            transform.position += velocity * Time.deltaTime;
        
    }


    public override void StartInteraction()
    {
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
