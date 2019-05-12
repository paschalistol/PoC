//Main Author: Emil Dahl

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField]private GameObject lockedDoor;
    private PhysicsScript body;
    [HideInInspector]public bool used = false;

    protected Vector3 velocity;
    protected BoxCollider boxCollider;
    private const float doorAngle = 90;
    private float doorOffset;
   // private Vector3 startPosition;


    protected const float skinWidth = 0.2f;

    private bool isHeld;

    void Start()
    {
        //GetComponent<RespawnItem>().startPosition = transform.position;
        Debug.Log("Logging");

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


            bool boxCast = Physics.BoxCast(transform.position, transform.localScale, Vector3.forward, out raycastHit, lockedDoor.transform.parent.rotation, 10f);
            if (raycastHit.collider != null && raycastHit.collider.transform.gameObject == lockedDoor)
            {

                //InteractionEvent interactedInfo = new InteractionEvent();
                //interactedInfo.eventDescription = "The door has been unlocked!";
                //interactedInfo.interactedObject = raycastHit.collider.transform.gameObject;

                //EventSystem.Current.FireEvent(interactedInfo);
                lockedDoor.GetComponent<Door>().InteractWithDoor();
                Destroy(gameObject);
                used = true;
            }

        }


     
    }

   

    public void KeyInteraction()
    {
        
        Debug.Log("walla does it work now?");
        if(isHeld == true)
        {
        transform.parent = null;
        }
        isHeld = !isHeld;
    }



    


}
