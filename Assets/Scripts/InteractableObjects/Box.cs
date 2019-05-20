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
        Debug.Log(velocity);
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
            //velocity = Vector3.up * bounceHeight + velocity.normalized;
            //velocity = velocity + new Vector3(0, bounceHeight, 0);
            velocity = new Vector3(velocity.x * 1.2f, bounceHeight, velocity.z * 1.2f);
            standOnTrampoline = false;
        }
    }

    public override void BeingThrown(Vector3 throwDirection)
    {
        velocity = throwDirection;
    }

    protected virtual void OnCollisionStay(Collision collision)
    {

        if (gameObject.CompareTag("Only Interaction") == false && collision.gameObject.layer != 0)
        {
            RespawnItem();
        }

    }

}