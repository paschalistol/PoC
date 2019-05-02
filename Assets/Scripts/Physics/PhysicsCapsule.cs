using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsCapsule : MonoBehaviour
{

    protected Vector3 velocity;
    protected CapsuleCollider capsuleCollider;
    private PhysicsScript whereAreMyDragons;
    protected const float skinWidth = 0.2f;
    // Start is called before the first frame update
    void Start()
    {
        whereAreMyDragons = GetComponent<PhysicsScript>();
        capsuleCollider = GetComponent<CapsuleCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        velocity = whereAreMyDragons.Gravity(velocity);
        velocity = whereAreMyDragons.CapsuleCollisionCheck(velocity, capsuleCollider, skinWidth);
        velocity = whereAreMyDragons.Decelerate(velocity);
        
        transform.position += velocity * Time.deltaTime;
    }
}
