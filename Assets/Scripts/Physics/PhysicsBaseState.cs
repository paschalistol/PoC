using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsBaseState : State
{

    protected PhysicsStateMachine owner;
    protected CapsuleCollider collider;
    protected const float skinWidth = 0.05f;
    protected Vector3 Velocity { get { return owner.velocity; } set { owner.velocity = value; } }
    protected Vector3 normal;
    protected const float staticFriction = 0.55f;
    protected float dynamicFriction;
    protected const float gravityConstant = 20f;

    public override void InitializeState(StateMachine owner)
    {
        this.owner = (PhysicsStateMachine)owner;
    }

    public override void EnterState()
    {
        collider = owner.GetComponent<CapsuleCollider>();
    }

    protected void CollisionCheck()
    {

        #region Raycast
        Vector3 point1 = owner.transform.position + collider.center + Vector3.up * (collider.height / 2 - collider.radius);
        Vector3 point2 = owner.transform.position + collider.center + Vector3.down * (collider.height / 2 - collider.radius);
        RaycastHit raycastHit;
        bool capsulecast = Physics.CapsuleCast(point1, point2,
            collider.radius, Velocity, out raycastHit, Velocity.magnitude * Time.deltaTime + skinWidth, owner.environment);
        #endregion

        if (raycastHit.collider == null)
            return;
        else
        {
            #region Apply Normal Force
            normal = Normal3D(Velocity, raycastHit.normal);
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

    private void Friction(float normalMag)
    {
        if (Velocity.magnitude < (staticFriction * normalMag))
        {
            Velocity = new Vector3(0, 0, 0);
        }
        else
        {
            Velocity += (dynamicFriction * normalMag) * -Velocity.normalized;
        }
    }

    protected void Gravity()
    {
        Vector3 gravity = Vector3.down * gravityConstant * Time.deltaTime;
        Velocity += gravity;

    }



}
