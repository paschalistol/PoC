//Main Author: Emil Dahl
//Secondary Author: Paschalis Tolios, Johan Ekman

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
    private const float doorAngle = 90;
    private float doorOffset;
    private GameObject parentEnemy;
    private Vector3 keyRelativePosition;
    private Quaternion keyRelativeRotation;


    private bool usedOnce = false;
    protected const float skinWidth = 0.2f;
    [SerializeField] private LayerMask door;

    protected override void Start()
    {

        boxCollider = GetComponent<BoxCollider>();

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
        //Debug.Log(parentEnemy.transform.position + "  " + keyStartPos);
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

    private void AddPhysics()
    {
        if (transform.parent == null && !isHeld)
        {
            Velocity = PhysicsScript.Decelerate(Velocity);
            Velocity = PhysicsScript.Gravity(Velocity);
            Velocity = PhysicsScript.CollisionCheck(Velocity, boxCollider, skinWidth, environment);

        }
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
        Physics.BoxCast(transform.position, transform.localScale, transform.forward, out raycastHit, lockedDoor.transform.parent.rotation, skinWidth * 3, door);
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
