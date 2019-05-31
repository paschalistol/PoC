//Main Author: Emil Dahl
//Secondary Author: Paschalis Tolios Johan Ekman

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : Interactable
{
    [SerializeField] private LayerMask environment;
    [SerializeField] private GameObject particles;

    private ParticleEvent startParticles;
    private StopParticleEvent stopParticles;

    public bool standOnTrampoline = false;
    protected const float skinWidth = 0.2f;
    [SerializeField] private float bounceHeight = 25;

    private bool usedOnce = false;
    [Header("Sounds")]
    [SerializeField] private AudioClip[] pickupSounds;
    public override AudioClip GetAudioClip()
    {

        return pickupSounds[Random.Range(0, pickupSounds.Length)];
    }
    protected override void Start() 
    {
        base.Start();

       
        isHeld = false;
    }
    void Update()
    {
        if (!GameController.isPaused)
        {
                AddPhysics();

                if (!usedOnce)
                {
                    ParticleStarter();
                }
            
            Bouncing();
        }


        transform.position += Velocity * Time.deltaTime;
    }





    public override void StartInteraction()
    {
        base.StartInteraction();
        isHeld = !isHeld;
        if (startParticles.particles != null) {
            usedOnce = false;
            ParticleStopper();
        }

    }


    private void AddPhysics()
    {
        if (!isHeld)
        {

        Velocity = PhysicsScript.Decelerate(Velocity);
        Velocity = PhysicsScript.Gravity(Velocity);
        }
        else
        {
            GetWallNormal();
        }
        Velocity = PhysicsScript.CollisionCheck(Velocity, boxCollider, skinWidth, environment);

    }

    private void ParticleStarter()
    {
        
        startParticles = new ParticleEvent();
        startParticles.objectPlaying = gameObject;
        startParticles.particles = particles;

        EventSystem.Current.FireEvent(startParticles);
        usedOnce = true;
    }

    private void ParticleStopper()
    {
        if (startParticles.particles == null)
            return;

        if (startParticles.particles != null)
        {
            stopParticles = new StopParticleEvent();
            stopParticles.particlesToStop = startParticles.particles;

            EventSystem.Current.FireEvent(stopParticles);
        }
    }




    protected void Bouncing()
    {
        if (standOnTrampoline)
        {
            Velocity = new Vector3(Velocity.x * 1.2f, bounceHeight, Velocity.z * 1.2f);
            standOnTrampoline = false;
        }
    }

    public override void BeingThrown(Vector3 throwDirection)
    {
        Velocity = throwDirection;
    }


}