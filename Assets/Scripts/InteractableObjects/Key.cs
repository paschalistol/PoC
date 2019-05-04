﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField]private GameObject lockedDoor;
    private PhysicsScript body;
    [HideInInspector]public bool used = false;

    protected Vector3 velocity;
    protected BoxCollider boxCollider;
    //private PhysicsScript whereAreMyDragons;
    protected const float skinWidth = 0.2f;

   // protected bool usingGravity;
    private bool isHeld;


    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
        body = gameObject.GetComponent<PhysicsScript>();
        isHeld = false;
    }
    void Update()
    {

        if (transform.parent == null && !isHeld)
        {
            velocity = body.Decelerate(velocity);
            velocity = body.Gravity(velocity);
            velocity = body.BoxCollisionCheck(velocity, boxCollider, skinWidth);
            transform.position += velocity * Time.deltaTime;
         }

        RaycastHit raycastHit;
        bool boxCast = Physics.BoxCast(transform.position, transform.localScale, Vector3.forward, out raycastHit, transform.rotation, transform.localScale.z + 3f);
        if (Input.GetKeyDown(KeyCode.E) && raycastHit.collider != null && raycastHit.collider.transform.gameObject == lockedDoor && !used)
        {
            gameObject.SetActive(false);
            //InteractionEvent interactedInfo = new InteractionEvent();
            //interactedInfo.eventDescription = "The door has been unlocked!";
            //interactedInfo.interactedObject = raycastHit.collider.transform.gameObject;

            //EventSystem.Current.FireEvent(interactedInfo);
            lockedDoor.GetComponent<Door>().InteractWithDoor();
            used = true;
        }
    }

   

    public void KeyInteraction()
    {
        if(isHeld == true)
        {
        transform.parent = null;

        }
        isHeld = !isHeld;
    }



    


}
