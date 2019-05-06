using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuseBoxItem : MonoBehaviour
{
    protected const float skinWidth = 0.2f;
    private PhysicsScript body;
    protected Vector3 velocity;
    protected BoxCollider boxCollider;
    [SerializeField] private GameObject lockedDoor;
    [SerializeField] private GameObject fuseBox;
    [SerializeField]private int itemQuantity = 2;
    private static int count;
    [HideInInspector]public bool isHeld;

    [SerializeField] private GameObject particles;
    [SerializeField] private GameObject endParticles;


    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
        body = gameObject.GetComponent<PhysicsScript>();
        isHeld = false;
    }
    void Update()
    {

        if (!isHeld)
        {
            velocity = body.Decelerate(velocity);
            velocity = body.Gravity(velocity);
            velocity = body.BoxCollisionCheck(velocity, boxCollider, skinWidth);
            transform.position += velocity * Time.deltaTime;
        }
        Debug.Log("Before raycast");
        RaycastHit raycastHit;
       
        bool boxCast = Physics.BoxCast(transform.position, transform.localScale, Vector3.forward, out raycastHit, transform.rotation, transform.localScale.z);

        if (Input.GetKeyDown(KeyCode.E) && raycastHit.collider != null && raycastHit.collider.transform.gameObject == fuseBox)
        {
            Debug.Log("After raycast");
            count++;
            //InteractionEvent interactedInfo = new InteractionEvent();
            //interactedInfo.eventDescription = "The door has been unlocked!";
            //interactedInfo.interactedObject = raycastHit.collider.transform.gameObject;

            //EventSystem.Current.FireEvent(interactedInfo);
            //fuseBox.GetComponent<FuseBox>().count++;
            //fuseBox.GetComponent<FuseBox>().InteractWithFuseBox();

            FuseBoxEvent fuseBoxEvent = new FuseBoxEvent();
            fuseBoxEvent.gameObject = gameObject;
            fuseBoxEvent.eventDescription = "Fusebox item: " + count;
            fuseBoxEvent.particles = particles;

            EventSystem.Current.FireEvent(fuseBoxEvent);

            if (count == itemQuantity)
            {
                //Run particles on fusebox activation of door
                lockedDoor.GetComponent<Door>().InteractWithDoor();

                OpenDoorEvent doorEvent = new OpenDoorEvent();
                Debug.Log("DoorOpened1");
                doorEvent.gameObject = gameObject;
                Debug.Log("DoorOpened2");
                doorEvent.eventDescription = "A door has been opened!";
                Debug.Log("DoorOpened3");
                doorEvent.particles = endParticles;
                Debug.Log("DoorOpened4");

                EventSystem.Current.FireEvent(doorEvent);

                Debug.Log("DoorOpened5");
              
            }
            gameObject.SetActive(false); 
        }
    }
}
