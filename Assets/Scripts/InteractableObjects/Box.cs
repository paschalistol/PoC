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

    protected const float skinWidth = 0.2f;

    private bool isHeld = false;
    [Header("Sounds")]
    [SerializeField] private AudioClip[] pickupSounds;
    public override AudioClip GetAudioClip()
    {

        return pickupSounds[Random.Range(0, pickupSounds.Length)];
    }
    void Start()
    {
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
    }

    public override void StartInteraction()
    {
        isHeld = !isHeld;
    }


}