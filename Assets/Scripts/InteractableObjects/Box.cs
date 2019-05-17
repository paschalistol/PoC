//Main Author: Emil Dahl
//Secondary Author: Paschalis Tolios

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
    protected float bounceHeight = 20;
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

    public virtual void ApplyForce(Vector3 vector)
    {
        velocity = vector;

    }

    protected void Bouncing()
    {
        if (standOnTrampoline)
        {
            ApplyForce(Vector3.up * bounceHeight);
            standOnTrampoline = false;
        }
    }

}