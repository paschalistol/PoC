////Main Author: Emil Dahl

//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;


//public class PhysicsCube : MonoBehaviour
//{
//    protected Vector3 velocity;
//    protected BoxCollider boxCollider;
//    private PhysicsScript body;
//    protected const float skinWidth = 0.05f;

//    void Start()
//    {
//        body = GetComponent<PhysicsScript>();
//        boxCollider = GetComponent<BoxCollider>();
//    }

//    void Update()
//    {
//        velocity = body.Decelerate(velocity);
//        velocity = body.Gravity(velocity);
//        velocity = body.CollisionCheck(velocity, boxCollider, skinWidth);
//        transform.position += velocity * Time.deltaTime;
//    }
//}
