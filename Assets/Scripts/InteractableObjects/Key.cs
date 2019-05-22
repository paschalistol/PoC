//Main Author: Emil Dahl
//Secondary Author: Paschalis Tolios

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Key : Interactable
{
    [SerializeField] private GameObject lockedDoor;
    [SerializeField] private GameObject[] unlockableDoors;
    [SerializeField] private GameObject particles;
    [SerializeField] private LayerMask environment;
    [HideInInspector] public bool used = false;

    private ParticleEvent startParticles;
    private StopParticleEvent stopParticles;
    protected Vector3 velocity;
    protected BoxCollider boxCollider;
    private const float doorAngle = 90;
    private float doorOffset;

    private bool usedOnce = false;
    protected const float skinWidth = 0.2f;
    [SerializeField] private LayerMask door;
    private bool isHeld;

    protected override void Start()
    {
        base.Start();
        boxCollider = GetComponent<BoxCollider>();

        isHeld = false;
    }
    void Update()
    {
        if (transform.parent == null && !isHeld)
        {
            AddPhysics();

            if (!usedOnce)
            {
                ParticleStarter();
            }
        }
        else if (isHeld)
        {
            UsingKeyCheck();
        }
    }
    public override void StartInteraction()
    {
        if (isHeld == true)
        {
            transform.parent = null;
        }
        isHeld = !isHeld;

        if (startParticles != null)
        {
            usedOnce = false;
            ParticleStopper();
        }
    }

    public override AudioClip GetAudioClip()
    {
        return null;
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
        stopParticles = new StopParticleEvent();
        stopParticles.particlesToStop = startParticles.particles;

        EventSystem.Current.FireEvent(stopParticles);
    }

        RaycastHit raycastHit;
    private void UsingKeyCheck()
    {
        Physics.BoxCast(transform.position, transform.localScale, transform.forward, out raycastHit, lockedDoor.transform.parent.rotation, skinWidth*3,door);
        if (raycastHit.collider != null && raycastHit.collider.transform.gameObject == lockedDoor)
        {
            lockedDoor.GetComponent<Interactable>().StartInteraction();
            Destroy(gameObject);
            used = true;
        }
    }
}
#region KeyLegacy
//GetComponent<RespawnItem>().startPosition = transform.position;
//float currentDoorRotation = lockedDoor.transform.parent.eulerAngles.y;
//float currentDoorPosition = lockedDoor.transform.parent.position.z - 5;
//float doorRotation = lockedDoor.transform.parent.rotation.y;

//if (currentDoorRotation > doorAngle)
//    //doorOffset = currentDoorRotation + 2f;
//else
//    //doorOffset = currentDoorRotation - 2f;
//InteractionEvent interactedInfo = new InteractionEvent();
//interactedInfo.eventDescription = "The door has been unlocked!";
//interactedInfo.interactedObject = raycastHit.collider.transform.gameObject;

//EventSystem.Current.FireEvent(interactedInfo);
#endregion
