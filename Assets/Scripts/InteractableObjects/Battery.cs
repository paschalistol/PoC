//Main Author: Paschalis Tolios
//Secondary author: Emil Dahl
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : MonoBehaviour
{
    
    public GameObject fuseBox;
    public GameObject lift;
    private bool used = false;
    [SerializeField] private GameObject particles;

    protected const float skinWidth = 0.1f;
    private PhysicsScript body;
    protected Vector3 velocity;
    protected BoxCollider boxCollider;
    [HideInInspector] public bool isHeld;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
        body = gameObject.GetComponent<PhysicsScript>();
        isHeld = false;
            Debug.Log("p: " + particles + "g: " + gameObject);
            Debug.Log("EventFired!");
    }


    private void Update()
    {

        Debug.Log("State of particles: " + particles);
        if (body.RespawnCollisionCheck(velocity, boxCollider))
        {
            RespawnEvent respawnEvent = new RespawnEvent();
            respawnEvent.gameObject = gameObject;

            EventSystem.Current.FireEvent(respawnEvent);
        }

        if (!isHeld)
        {
            velocity = body.Decelerate(velocity);
            velocity = body.Gravity(velocity);
            velocity = body.CollisionCheck(velocity, boxCollider, skinWidth);
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

            ParticleEvent particleEvent = new ParticleEvent();
            particleEvent.eventDescription = "Particles Created!";
            particleEvent.objectPlaying = gameObject;
            particleEvent.particles = particles;

            EventSystem.Current.FireEvent(particleEvent);

            Destroy(gameObject);
            // used = true;
        }
    }
}
