//Author: Paschalis Tolios

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boxes : MonoBehaviour
{

    private Vector3 velocity;
    float gravityConstant = 12;
    BoxCollider boxCollider;
    private float skinWidth = 0.007f;
    Vector3 normal;
    public LayerMask layerMask, waterMask;
    private float staticFriction = 0.8f, dynamicFriction = 0.7f;
    private Vector3 collisionPoint;
    private PhysicsScript body;
    private bool isHeld,collided = false;
    GeneralFunctions generalFunctions;
    private void Awake()
    {
        boxCollider = gameObject.GetComponent<BoxCollider>();
        body = gameObject.GetComponent<PhysicsScript>();
        isHeld = false;
        generalFunctions = gameObject.GetComponent<GeneralFunctions>();
    }
    private void Update()
    {
        if (collided && !isHeld)
        {
            transform.position = collisionPoint;
            collided = false;
        }
        if (!isHeld)
        {
            velocity = body.Decelerate(velocity);
            velocity = body.Gravity(velocity);
            velocity = body.CollisionCheck(velocity, boxCollider, skinWidth);
            transform.position += velocity * Time.deltaTime;
        }
        Debug.Log("collided: " + collided + "    isheld: "+ isHeld);
    }

    public void BoxInteraction()
    {

        isHeld = !isHeld;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("collided!");
        collided = true;
        collisionPoint = transform.position;
    }

   
}
