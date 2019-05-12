//Main Author: Emil Dahl

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    private PhysicsScript body;
    protected Vector3 velocity;
    protected BoxCollider boxCollider;

    protected const float skinWidth = 0.2f;

    private bool isHeld = false;

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
            velocity = body.CollisionCheck(velocity, boxCollider, skinWidth);
            transform.position += velocity * Time.deltaTime;
        }
    }

    public void BoxInteraction()
    {

        isHeld = !isHeld;
    }






}