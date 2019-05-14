//Main Author: Emil Dahl
//Secondary Author: Paschalis Tolios

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : Interactable
{
    [SerializeField] private GameObject lockedDoor;
    private PhysicsScript body;
    [HideInInspector] public bool used = false;

    protected Vector3 velocity;
    protected BoxCollider boxCollider;
    private const float doorAngle = 90;
    private float doorOffset;

    protected const float skinWidth = 0.2f;

    private bool isHeld;

    void Start()
    {
        //GetComponent<RespawnItem>().startPosition = transform.position;
        boxCollider = GetComponent<BoxCollider>();
        body = gameObject.GetComponent<PhysicsScript>();
        isHeld = false;
    }
    void Update()
    {

        if (body.RespawnCollisionCheck(velocity, boxCollider))
        {
            RespawnEvent respawnEvent = new RespawnEvent();
            respawnEvent.gameObject = gameObject;

            EventSystem.Current.FireEvent(respawnEvent);
        }


        RaycastHit raycastHit;
        if (transform.parent == null && !isHeld)
        {
            velocity = body.Decelerate(velocity);
            velocity = body.Gravity(velocity);
            velocity = body.CollisionCheck(velocity, boxCollider, skinWidth);
            transform.position += velocity * Time.deltaTime;
        }
        else if (isHeld)
        {
            //float currentDoorRotation = lockedDoor.transform.parent.eulerAngles.y;
            //float currentDoorPosition = lockedDoor.transform.parent.position.z - 5;
            //float doorRotation = lockedDoor.transform.parent.rotation.y;

            //if (currentDoorRotation > doorAngle)
            //    //doorOffset = currentDoorRotation + 2f;
            //else
            //    //doorOffset = currentDoorRotation - 2f;


            bool boxCast = Physics.BoxCast(transform.position, transform.localScale, transform.forward, out raycastHit, lockedDoor.transform.parent.rotation, skinWidth);
            if (raycastHit.collider != null && raycastHit.collider.transform.gameObject == lockedDoor)
            {

                //InteractionEvent interactedInfo = new InteractionEvent();
                //interactedInfo.eventDescription = "The door has been unlocked!";
                //interactedInfo.interactedObject = raycastHit.collider.transform.gameObject;

                //EventSystem.Current.FireEvent(interactedInfo);
                lockedDoor.GetComponent<Interactable>().StartInteraction();
                Destroy(gameObject);
                used = true;
            }

        }
    }



    public override void StartInteraction()
    {
        if (isHeld == true)
        {
            transform.parent = null;
        }
        isHeld = !isHeld;
    }

    public override AudioClip GetAudioClip()
    {
        return null;
    }
}
