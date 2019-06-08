//Main Author: Emil Dahl
//Secondary Author: Paschalis Tolios, Johan Ekman

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class handles the key behavior
/// </summary>
[RequireComponent(typeof(BoxCollider))]
public class Key : Interactable
{
    [SerializeField] private GameObject lockedDoor;
    [SerializeField] private GameObject[] unlockableDoors;
    [SerializeField] private GameObject particles;
    [SerializeField] private LayerMask environment;
    [SerializeField] private LayerMask door;
    [HideInInspector] public bool used = false;

    private ParticleEvent startParticles;
    private StopParticleEvent stopParticles;
    private GameObject parentEnemy;
    private Vector3 keyRelativePosition;
    private Quaternion keyRelativeRotation;
    private const float doorAngle = 90;
    protected const float skinWidth = 0.2f;
    private float doorOffset;
    private bool usedOnce = false;

    protected override void Start()
    {

        base.Start();
        isHeld = false;
        if (gameObject.transform.parent != null && gameObject.tag == "Key")
        {
            parentEnemy = transform.parent.gameObject;
            keyRelativePosition = transform.localPosition;
            keyRelativeRotation = transform.localRotation;

        }
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
            UsingKeyCheck();
            transform.position += Velocity * Time.deltaTime;
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

    public override void RespawnItem()
    {
        base.RespawnItem();
        transform.parent = parentEnemy.transform;
        transform.localPosition = keyRelativePosition;
        transform.localRotation = keyRelativeRotation;

    }

    public override AudioClip GetAudioClip()
    {
        return null;
    }

    /// <summary>
    /// Activates gravity and deceleration on set object if not held, and checks for collision all the time
    /// </summary>
    private void AddPhysics()
    {
        if (transform.parent == null)
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
    }

    /// <summary>
    /// Fires particle event on the key
    /// </summary>
    private void ParticleStarter()
    {
        startParticles = new ParticleEvent();
        startParticles.objectPlaying = gameObject;
        startParticles.particles = particles;

        EventSystem.Current.FireEvent(startParticles);
        usedOnce = true;
    }

    /// <summary>
    /// Fires event to stop particles on the key
    /// </summary>
    private void ParticleStopper()
    {
        stopParticles = new StopParticleEvent();
        stopParticles.particlesToStop = startParticles.particles;

        EventSystem.Current.FireEvent(stopParticles);
    }

    /// <summary>
    /// If there is something in front of the door, open it
    /// </summary>
    private void UsingKeyCheck()
    {
        if (isHeld)
        {
            Physics.BoxCast(transform.position, transform.localScale, transform.forward, out raycastHit, boxCollider.transform.rotation, skinWidth * 3, door);
            if (raycastHit.collider != null && raycastHit.collider.transform.gameObject == lockedDoor)
            {
                UnlockDoor();
            }
            Physics.BoxCast(transform.position, transform.localScale, transform.right, out raycastHit, boxCollider.transform.rotation, skinWidth * 3, door);
            if (raycastHit.collider != null && raycastHit.collider.transform.gameObject == lockedDoor)
            {
                UnlockDoor();
            }
            Physics.BoxCast(transform.position, transform.localScale, transform.right *-1, out raycastHit, boxCollider.transform.rotation, skinWidth * 3, door);
            if (raycastHit.collider != null && raycastHit.collider.transform.gameObject == lockedDoor)
            {
                UnlockDoor();
            }
        }
    }

    /// <summary>
    /// Unlocks the door and deletes the key
    /// </summary>
    private void UnlockDoor()
    {
        if (raycastHit.collider != null && raycastHit.collider.transform.gameObject == lockedDoor)
        {
            lockedDoor.GetComponent<Door>().UnlockDoor();
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
