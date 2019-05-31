//Main Author: Paschalis Tolios
//Secondary author: Emil Dahl

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : Interactable
{
    [SerializeField] private LayerMask environment;
    public GameObject fuseBox;
    public GameObject lift;
    private bool used = false;
    [SerializeField] private GameObject onfuseboxActivationParticles;

    protected const float skinWidth = 0.1f;

    protected Vector3 velocity;


    protected override void Start()
    {
        base.Start();
        boxCollider = GetComponent<BoxCollider>();
        isHeld = false;


    }


        new RaycastHit raycastHit;
    private void Update()
    {

            AddPhysics();
            transform.position += velocity * Time.deltaTime;

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
        isHeld = !isHeld;
    }

    public override AudioClip GetAudioClip()
    {
        return null;
    }
}
