﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsCapsule : MonoBehaviour
{

    protected Vector3 velocity;
    protected CapsuleCollider capsuleCollider;
    private PhysicsScript body;
    protected const float skinWidth = 0.2f;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<PhysicsScript>();
        capsuleCollider = GetComponent<CapsuleCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        velocity = body.Gravity(velocity);
        velocity = body.CapsuleCollisionCheck(velocity, capsuleCollider, skinWidth);
        velocity = body.Decelerate(velocity);
        
        transform.position += velocity * Time.deltaTime;
    }
}
