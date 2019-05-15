//Main Author: Emil Dahl
//Secondary Author: Paschalis Tolios

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuseBoxItem : Interactable
{
    protected const float skinWidth = 0.2f;
    protected Vector3 velocity;
    protected BoxCollider boxCollider;
    [SerializeField] private GameObject lockedDoor;
    [SerializeField] private GameObject fuseBox;
    [SerializeField]private int itemQuantity = 2;
    private static int count;
    [HideInInspector]public bool isHeld;

    //[SerializeField] private GameObject particles;
    //[SerializeField] private GameObject endParticles;


    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
        
        isHeld = false;
    }
    void Update()
    {
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
       
        bool boxCast = Physics.BoxCast(transform.position, transform.localScale, transform.forward, out raycastHit, transform.rotation, transform.localScale.z);

        if (raycastHit.collider != null && raycastHit.collider.transform.gameObject == fuseBox)
        {    
            count++;
        
            FuseBoxEvent fuseBoxEvent = new FuseBoxEvent();
            fuseBoxEvent.gameObject = gameObject;
            fuseBoxEvent.eventDescription = "Fusebox item: " + count;
            //fuseBoxEvent.particles = particles;

            EventSystem.Current.FireEvent(fuseBoxEvent);
                Debug.Log("Running update!");

            if (count == itemQuantity)
            {
                //Run particles on fusebox activation of door
                lockedDoor.GetComponent<Door>().StartInteraction();
                OpenDoorEvent doorEvent = new OpenDoorEvent();
                doorEvent.gameObject = gameObject;
                doorEvent.eventDescription = "A door has been opened!";
                //doorEvent.particles = endParticles;

                EventSystem.Current.FireEvent(doorEvent);
            }
            Destroy(gameObject); 
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
