//Author: Johan Ekman
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gold : MonoBehaviour
{

    protected const float skinWidth = 0.2f;
    private PhysicsScript body;
    protected Vector3 velocity;
    protected BoxCollider boxCollider;
    public LayerMask goal;
    [HideInInspector] public bool isHeld;


    void Start()
    {
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

        if (!isHeld)
        {
            velocity = body.Decelerate(velocity);
            velocity = body.Gravity(velocity);
            velocity = body.CollisionCheck(velocity, boxCollider, skinWidth);
            transform.position += velocity * Time.deltaTime;
        }

        RaycastHit raycastHit;

        bool boxCast = Physics.BoxCast(transform.position, transform.localScale, Vector3.forward, out raycastHit, transform.rotation, transform.localScale.z, goal);

        if (raycastHit.collider != null)
        {
            WinningEvent winInfo = new WinningEvent();
            winInfo.eventDescription = "You won!";

            //winInfo.gameObject = owner.transform.gameObject;
            EventSystem.Current.FireEvent(winInfo);

        }
    }
}
