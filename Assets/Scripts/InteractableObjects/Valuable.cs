//Main Author: Johan Ekman
//Secondary Author: Paschalis Tolios

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Valuable : Interactable
{

    public float value;
    protected Vector3 velocity;
    protected BoxCollider boxCollider;
    protected const float skinWidth = 0.2f;
    [SerializeField] private LayerMask environment;


    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
        
        

    }

    
    void Update()
    {
        
            velocity = PhysicsScript.Decelerate(velocity);
            velocity = PhysicsScript.Gravity(velocity);
            velocity = PhysicsScript.CollisionCheck(velocity, boxCollider, skinWidth, environment);
            transform.position += velocity * Time.deltaTime;
        
    }


    public override void StartInteraction()
    {
        AddPointEvent addPointInfo = new AddPointEvent();
        addPointInfo.eventDescription = "Getting points!";
        addPointInfo.point = value;
        EventSystem.Current.FireEvent(addPointInfo);
        Destroy(gameObject);
    }

    public override AudioClip GetAudioClip()
    {
        return null;
    }
}
