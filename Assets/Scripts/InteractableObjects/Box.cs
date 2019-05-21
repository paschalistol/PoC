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

    protected Vector3 velocity;
    protected BoxCollider boxCollider;
    public bool standOnTrampoline = false;
    protected const float skinWidth = 0.2f;
    [SerializeField] private float bounceHeight = 25;
    private bool isHeld = false;
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
        boxCollider = GetComponent<BoxCollider>();
       
        isHeld = false;

    }

    void Update()
    {
        if (!isHeld)
        {
            AddPhysics();

            if (!usedOnce)
            {
                ParticleStarter();
            }
        }
        Bouncing();
        
    }

    public override void StartInteraction()
    {
        isHeld = !isHeld;

        if (startParticles.particles != null) {
            usedOnce = false;
            ParticleStopper();
        }
    }

    private void AddPhysics()
    {
        velocity = PhysicsScript.Decelerate(velocity);
        velocity = PhysicsScript.Gravity(velocity);
        velocity = PhysicsScript.CollisionCheck(velocity, boxCollider, skinWidth, environment);
        transform.position += velocity * Time.deltaTime;
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
            Debug.Log("Stopped particles");
        }
    }



    protected void Bouncing()
    {
        if (standOnTrampoline)
        {
            velocity = new Vector3(velocity.x * 1.2f, bounceHeight, velocity.z * 1.2f);
            standOnTrampoline = false;
        }
    }

    public override void BeingThrown(Vector3 throwDirection)
    {
        velocity = throwDirection;
    }


}