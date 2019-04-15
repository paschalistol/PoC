using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : MonoBehaviour
{
    private Vector3 velocity;
    float gravityConstant = 12;
    CapsuleCollider capsuleCollider;
    private float skinWidth = 0.007f;
    Vector3 normal;
    public LayerMask layerMask;
    private float staticFriction = 0.8f, dynamicFriction = 0.7f;


    GeneralFunctions generalFunctions;
    private void Awake()
    {
        capsuleCollider = gameObject.GetComponent<CapsuleCollider>();

        generalFunctions = gameObject.GetComponent<GeneralFunctions>();
    }
    private void Update()
    {
        Gravity();
        
        //CollisionCheck();
        transform.position += velocity * Time.deltaTime;
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

    private void Friction(float normalMag)
    {
        if (velocity.magnitude < (staticFriction * normalMag))
        {
            velocity = new Vector3(0, 0, 0);
        }
        else
        {
            velocity += (dynamicFriction * normalMag) * -velocity.normalized;
        }
    }

    /*void CollisionCheck()
    {

        #region Raycast
        Vector3 point1 = transform.position + capsuleCollider.center + Vector3.up * (capsuleCollider.height / 2 - capsuleCollider.radius);
        Vector3 point2 = transform.position + capsuleCollider.center + Vector3.down * (capsuleCollider.height / 2 - capsuleCollider.radius);
        RaycastHit raycastHit;
        bool capsulecast = Physics.CapsuleCast(point1, point2, capsuleCollider.radius, Velocity, out raycastHit, Velocity.magnitude * Time.deltaTime + skinWidth, owner.environment);
        #endregion

        if (raycastHit.collider == null)
            return;
        else
        {
            #region Apply Normal Force
            normal = generalFunctions.Normal3D(Velocity, raycastHit.normal);
            Velocity += normal;
            Friction(normal.magnitude);
            #endregion
            if (Velocity.magnitude < skinWidth)
            {
                Velocity = Vector3.zero;
                return;
            }

            CollisionCheck();
        }
    }*/
}
