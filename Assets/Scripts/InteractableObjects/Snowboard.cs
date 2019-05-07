//Main Author: Johan Ekman
//Secondary authors: Emil Dahl, Johan Ekman

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snowboard : MonoBehaviour
{
    public GameObject player;
    public BoxCollider box;
    public LayerMask layer;
    private bool usingSnowboard = false;
    
    
    private Vector3 velocity;
    float gravityConstant = 12;
    private float groundCheck = 0.00f, skinWidth = 0.001f;
    private float staticFriction = 0.8f, dynamicFriction = 0.7f;
    public LayerMask layerMask;
   GeneralFunctions generalFunctions;

    void Awake()
    {
        box = gameObject.GetComponent<BoxCollider>();

        generalFunctions = GetComponent<GeneralFunctions>();
    }


    void Update()
    {


        Gravity();
        CollisionCheck();
        /*


        Vector3 angle = (Mathf.Cos(transform.rotation.x)*Vector3.down).normalized;
        Debug.Log((transform.eulerAngles.x));
        Debug.DrawRay(transform.position, angle, Color.magenta);



    */
       transform.position += velocity * Time.deltaTime;

    }

    private void Rotate(RaycastHit raycastHit)
    {
        Vector3 objectForward = transform.TransformDirection(Vector3.forward);
        float step = 200;
        Quaternion rotation = Quaternion.RotateTowards(transform.rotation, raycastHit.collider.transform.rotation,step);
        transform.rotation = rotation;
    }
   
    
     private void Gravity()
    {
        Vector3 gravity = Vector3.down * gravityConstant * Time.deltaTime;
        velocity += gravity;
    }



    public void ApplyForce(Vector3 force)
    {
        velocity += force;
    }



    public void CollisionCheck()
    {

        RaycastHit raycastHit;

        bool boxCast = Physics.BoxCast(transform.position, transform.localScale, velocity, out raycastHit, transform.rotation, velocity.magnitude * Time.deltaTime + skinWidth, layerMask);


        if (raycastHit.collider == null)
            return;
        else
        {
          //  Debug.Log(raycastHit.distance);
            Rotate(raycastHit);
            Vector3 normal;
            #region Apply Normal Force
            normal = generalFunctions.Normal3D(velocity, raycastHit.normal);
            velocity += normal;
            #endregion
            generalFunctions.Friction(normal.magnitude, staticFriction, dynamicFriction, velocity);
            if (velocity.magnitude < skinWidth)
            {
                velocity = Vector3.zero;
                return;
            }

            //CollisionCheck();
        }
    }

    public void SetVelocity(Vector3 v)
    {
        velocity += v;
        Debug.Log(velocity);
    }
}
