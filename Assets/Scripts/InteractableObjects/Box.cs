//Main Author: Emil Dahl
//Secondary Author: Paschalis Tolios Johan Ekman

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : Interactable
{
    [SerializeField] private LayerMask environment;
    protected Vector3 velocity;
    protected BoxCollider boxCollider;
    public bool standOnTrampoline = false;
    protected const float skinWidth = 0.2f;
    protected float bounceHeight = 25;
    private bool isHeld = false;
    [Header("Sounds")]
    [SerializeField] private AudioClip[] pickupSounds;
    public override AudioClip GetAudioClip()
    {

        return pickupSounds[Random.Range(0, pickupSounds.Length)];
    }
    protected override void Start() 
    {
        base.Start();
        boxCollider = GetComponent<BoxCollider>();
       
        isHeld = false;

    }

    void Update()
    {
        if (!isHeld)
        {
            velocity = PhysicsScript.Decelerate(velocity);
            velocity = PhysicsScript.Gravity(velocity);
            velocity = PhysicsScript.CollisionCheck(velocity, boxCollider, skinWidth, environment);
            transform.position += velocity * Time.deltaTime;
        }

        Bouncing();
    }

    public override void StartInteraction()
    {
        isHeld = !isHeld;
    }

 

    protected void Bouncing()
    {
        if (standOnTrampoline)
        {
            velocity = new Vector3(velocity.x * 1.18f, bounceHeight, velocity.z * 1.18f);
            standOnTrampoline = false;
        }
    }

    public override void BeingThrown(Vector3 throwDirection)
    {
        velocity = throwDirection;
    }


}