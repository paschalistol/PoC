using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lift : MonoBehaviour
{
    private Vector3 velocity;
    
    /*BoxCollider boxCollider;
    private float skinWidth = 0.007f;
    Vector3 normal;
    public LayerMask layerMask;
    private float staticFriction = 0.8f, dynamicFriction = 0.7f;
    GeneralFunctions generalFunctions;*/
    public GameObject destination;

    private void Awake()
    {
       /* boxCollider = gameObject.GetComponent<BoxCollider>();

        generalFunctions = gameObject.GetComponent<GeneralFunctions>();*/

    }

    void Update()
    {
        
        
        
            MoveToDestination();
        

        /*CollisionCheck();
        transform.position += velocity * Time.deltaTime;*/
    }

    



   /* public void ApplyForce(Vector3 force)
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

    void CollisionCheck()
    {

        RaycastHit raycastHit;

        bool boxCast = Physics.BoxCast(transform.position, transform.localScale, Vector3.down, out raycastHit, transform.rotation, velocity.magnitude * Time.deltaTime + skinWidth, layerMask);


        if (raycastHit.collider == null)
            return;
        else
        {

            #region Apply Normal Force
            normal = generalFunctions.Normal3D(velocity, raycastHit.normal);
            velocity += normal;
            #endregion
            Friction(normal.magnitude);
            if (velocity.magnitude < skinWidth)
            {
                velocity = Vector3.zero;
                return;
            }

            //CollisionCheck();
        }
    }*/

    void MoveToDestination()
    {

        Vector3 direction = destination.transform.position - transform.position;
        if (transform.position != destination.transform.position && direction.magnitude > 0.1f)
        {
            
            transform.position += direction.normalized * 5f * Time.deltaTime;
        }
    }
}


