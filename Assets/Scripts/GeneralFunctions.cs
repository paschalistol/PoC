using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralFunctions : MonoBehaviour
{
    public Vector2 NormalForce(Vector2 velocity, Vector2 normal)
    {
        float dotProduct = Vector2.Dot(velocity, normal);
        if (dotProduct > 0)
        {
            dotProduct = 0f;
        }
        Vector2 projection = dotProduct * normal;
        return -projection;

    }
    public Vector3 Normal3D(Vector3 velocity, Vector3 normal)
    {
        
        float dotProduct = Vector3.Dot(velocity, normal);
        
        if (dotProduct > 0)
        {
            dotProduct = 0f;
        }
        Vector3 projection = dotProduct * normal;
        return -projection;
    }
    public Vector3 Friction(float normalMag, float staticF, float dynamicF, Vector3 velocity)
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
}
