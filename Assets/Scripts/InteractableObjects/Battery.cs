﻿//Main Author: Paschalis Tolios
//Secondary author: Emil Dahl

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : Interactable
{

    public GameObject fuseBox;
    public GameObject lift;
    private bool used = false;
    [SerializeField] private GameObject onfuseboxActivationParticles;

    protected const float skinWidth = 0.1f;

    protected Vector3 velocity;
    protected BoxCollider boxCollider;
    [HideInInspector] public bool isHeld;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
        isHeld = false;
        Debug.Log("p: " + onfuseboxActivationParticles + "g: " + gameObject);
        Debug.Log("EventFired!");
    }


    private void Update()
    {

        Debug.Log("State of particles: " + onfuseboxActivationParticles);
        if (PhysicsScript.physics.RespawnCollisionCheck(velocity, boxCollider))
        {
            RespawnEvent respawnEvent = new RespawnEvent();
            respawnEvent.gameObject = gameObject;

            EventSystem.Current.FireEvent(respawnEvent);
        }

        if (!isHeld)
        {
            velocity = PhysicsScript.physics.Decelerate(velocity);
            velocity = PhysicsScript.physics.Gravity(velocity);
            velocity = PhysicsScript.physics.CollisionCheck(velocity, boxCollider, skinWidth);
            transform.position += velocity * Time.deltaTime;
        }

        RaycastHit raycastHit;
        bool boxCast = Physics.BoxCast(transform.position, transform.localScale, Vector3.down, out raycastHit, transform.rotation, transform.localScale.y + 0.003f);
        if (raycastHit.collider != null && raycastHit.collider.transform.gameObject == fuseBox)
        {

            InteractionEvent interactedInfo = new InteractionEvent();
            interactedInfo.eventDescription = "Pressed item has been activated: ";
            interactedInfo.interactedObject = gameObject;

            lift.GetComponent<Lift2>().onOff = true;
            EventSystem.Current.FireEvent(interactedInfo);

            //ParticleEvent particleEvent = new ParticleEvent();
            //particleEvent.eventDescription = "Particles Created!";
            //particleEvent.objectPlaying = gameObject;
            //particleEvent.particles = onfuseboxActivationParticles;

            //EventSystem.Current.FireEvent(particleEvent);

            Destroy(gameObject);
            // used = true;
        }
    }

    public override void StartInteraction()
    {
        isHeld = !isHeld;
    }

    public override AudioClip GetAudioClip()
    {
        return null;
    }
}
