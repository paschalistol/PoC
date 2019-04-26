using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boxes : MonoBehaviour
{

    private Vector3 velocity;
    float gravityConstant = 12;
    BoxCollider boxCollider;
    private float skinWidth = 0.007f;
    Vector3 normal;
    public LayerMask layerMask, waterMask;
    private float staticFriction = 0.8f, dynamicFriction = 0.7f;


    GeneralFunctions generalFunctions;
    private void Awake()
    {
        boxCollider = gameObject.GetComponent<BoxCollider>();

        generalFunctions = gameObject.GetComponent<GeneralFunctions>();
    }
    private void Update()
    {
        Gravity();
        Float();
        CollisionCheck();
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
    }

    void Float()
    {
        RaycastHit raycastHit;

        bool boxCast = Physics.BoxCast(transform.position, transform.localScale, Vector3.down, out raycastHit, transform.rotation, velocity.magnitude * Time.deltaTime + skinWidth, waterMask);

        if (raycastHit.collider == null)
            return;
        else
        {


            #region Apply Normal Force
            normal = generalFunctions.Normal3D(velocity, raycastHit.normal);
            velocity += normal;
            #endregion
            Friction(normal.magnitude);
            if (raycastHit.distance< skinWidth)
            {
                velocity = Vector3.zero;
                return;
            }

            Float();
        }
    }
}
