using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gold : Interactable
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
        boxCollider = GetComponent<BoxCollider>();

        isHeld = false;

    }

    void Update()
    {

            AddPhysics();
            transform.position += Velocity * Time.deltaTime;

            if (!usedOnce)
            {
                startParticles = new ParticleEvent();
                startParticles.objectPlaying = gameObject;
                startParticles.particles = particles;

                EventSystem.Current.FireEvent(startParticles);
                usedOnce = true;
            }
        
        Bouncing();

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

    public override void StartInteraction()
    {
        base.StartInteraction();
        isHeld = !isHeld;
        usedOnce = false;
        GameController.activatedAlarm = true;

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
