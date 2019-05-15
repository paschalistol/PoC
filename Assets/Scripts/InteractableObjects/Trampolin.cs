using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampolin : Interactable
{


    [HideInInspector] public bool used = false;

    protected Vector3 velocity;
    protected BoxCollider boxCollider;

    protected const float skinWidth = 0.2f;

    private bool isHeld;
    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();

        isHeld = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isHeld)
        {
            velocity = PhysicsScript.physics.Decelerate(velocity);
            velocity = PhysicsScript.physics.Gravity(velocity);
            velocity = PhysicsScript.physics.CollisionCheck(velocity, boxCollider, skinWidth);
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
}
