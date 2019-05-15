//Main Author: Emil Dahl

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PhysicsScript
{

    private static float deceleration = 3;
    private static Vector3 normal;
    private static  float staticFriction = 0.55f;
    private static float dynamicFriction;
    private static  float gravityConstant = 5f;



    public static Vector3 CollisionCheck(Vector3 velocity, BoxCollider collider, float skinWidth, LayerMask layerMask)
    {
        RaycastHit raycastHit;
        #region Raycast

        bool boxCast = Physics.BoxCast(collider.transform.position, collider.transform.localScale / 2,
            velocity, out raycastHit, collider.transform.rotation, velocity.magnitude * Time.deltaTime + skinWidth, layerMask);
        #endregion
        if (raycastHit.collider == null)
            return velocity;
        else
        {
            #region Apply Normal Force
            normal = Normal3D(velocity, raycastHit.normal);
            velocity += normal;
            Friction(normal.magnitude, velocity);
            #endregion
            if (velocity.magnitude < skinWidth)
            {
                velocity = Vector3.zero;
                return velocity;
            }

            CollisionCheck(velocity, collider, skinWidth, layerMask);
            return velocity;
        }
    }

    public static Vector3 CollisionCheck(Vector3 velocity, CapsuleCollider collider, float skinWidth, LayerMask layerMask)
    {
        RaycastHit raycastHit;
        #region Raycast
        Vector3 point1 = collider.transform.position + collider.center + collider.transform.up * (collider.height / 2 - collider.radius);
        Vector3 point2 = collider.transform.position + collider.center - collider.transform.up * (collider.height / 2 - collider.radius);
        bool capsulecast = Physics.CapsuleCast(point1, point2,
            collider.radius, velocity, out raycastHit, velocity.magnitude * Time.deltaTime + skinWidth, layerMask);
        #endregion
        if (raycastHit.collider == null)
            return velocity;
        else
        {
            #region Apply Normal Force
            normal = Normal3D(velocity, raycastHit.normal);
            velocity += normal;
            Friction(normal.magnitude, velocity);
            #endregion
            if (velocity.magnitude < skinWidth)
            {
                velocity = Vector3.zero;
                return velocity;
            }

            CollisionCheck(velocity, collider, skinWidth, layerMask);
            return velocity;
        }

    }


    public static Vector3 Normal3D(Vector3 velocity, Vector3 normal)
    {

        float dotProduct = Vector3.Dot(velocity, normal);

        if (dotProduct > 0)
        {
            dotProduct = 0f;
        }
        Vector3 projection = dotProduct * normal;
        return -projection;
    }

    private static void Friction(float normalMag, Vector3 velocity)
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

    public static Vector3 Friction(float normalMag, float staticF, float dynamicF, Vector3 velocity)
    {
        if (velocity.magnitude < (staticF * normalMag))
        {
            velocity = new Vector3(0, 0, 0);
        }
        else
        {
            velocity += (dynamicF * normalMag) * -velocity.normalized;
        }
        return velocity;
    }
    public static Vector3 Gravity(Vector3 velocity)
    {
        Vector3 gravity = Vector3.down * gravityConstant * Time.deltaTime;
        velocity += gravity;



        return velocity;
    }

    public static Vector3 Decelerate(Vector3 velocity)
    {
        Vector3 tempVel = new Vector3(velocity.x, 0, velocity.z);
        //Vector3 tempVel = new Vector3(Velocity.x, Velocity.y, Velocity.z);
        velocity -= tempVel.normalized * deceleration * Time.deltaTime;
        return velocity;
    }

    public static Vector2 NormalForce(Vector2 velocity, Vector2 normal)
    {
        float dotProduct = Vector2.Dot(velocity, normal);
        if (dotProduct > 0)
        {
            dotProduct = 0f;
        }
        Vector2 projection = dotProduct * normal;
        return -projection;

    }


}
