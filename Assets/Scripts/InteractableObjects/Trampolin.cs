﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampolin : Interactable
{


    [HideInInspector] public bool used = false;

    protected Vector3 velocity;
    protected BoxCollider boxCollider;
    [SerializeField] private LayerMask environment;
    protected const float skinWidth = 0.2f;
    private BoxCollider collider;

    private bool isHeld;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        boxCollider = GetComponent<BoxCollider>();

        isHeld = false;
    }

    // Update is called once per frame
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

    public override AudioClip GetAudioClip()
    {
        return null;
    }

    public override void StartInteraction()
    {
        Debug.Log("StartingInteraction");
        isHeld = !isHeld;
    }

    public void Bounce()
    {
        RaycastHit raycastHit;
        #region Raycast

        Physics.BoxCast(collider.transform.position, collider.transform.localScale / 2,
            velocity, out raycastHit, collider.transform.rotation, velocity.magnitude * Time.deltaTime + skinWidth, layerMask);
        #endregion
        if (raycastHit.collider == null)
            return velocity;
    }

}
