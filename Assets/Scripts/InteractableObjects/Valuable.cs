//Author: Johan Ekman
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Valuable : MonoBehaviour
{

    public float value;
    protected Vector3 velocity;
    protected BoxCollider boxCollider;
    protected const float skinWidth = 0.2f;
    public  PhysicsScript body;
    


    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
        body = gameObject.GetComponent<PhysicsScript>();
        

    }

    
    void Update()
    {
        
            velocity = body.Decelerate(velocity);
            velocity = body.Gravity(velocity);
            velocity = body.CollisionCheck(velocity, boxCollider, skinWidth);
            transform.position += velocity * Time.deltaTime;
        
    }

    public void AddPoint()
    {
        Debug.Log("0");
        AddPointEvent addPointInfo = new AddPointEvent();
        addPointInfo.eventDescription = "Getting points!";
        addPointInfo.point = value;
        EventSystem.Current.FireEvent(addPointInfo);
        Destroy(gameObject);

    }
}
