﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterBaseState : State
{
    protected CharacterStateMachine owner;

    protected CapsuleCollider capsuleCollider;
    protected GeneralFunctions generalFunctions;
    protected const int acceleration = 23;
    protected const float skinWidth = 0.003f;
    protected const float gravityConstant = 20f;
    protected const float groundCheckDistance = 0.3f;
    protected Vector3 Velocity { get { return owner.velocity; } set { owner.velocity = value; } }
    protected float MaxSpeed { get { return owner.maxSpeed; } set { owner.maxSpeed = value; } }
    protected const float deceleration = 5;
    protected Vector2 airResistance;
    protected const float staticFriction = 0.55f;
    protected float dynamicFriction ;
    protected const float airCoeff = 0.4f;
    protected Vector3 normal;
    protected const float jumpHeight = 12;
    protected bool snowboarding = false;

    

    public override void InitializeState(StateMachine owner)
    {
        this.owner = (CharacterStateMachine)owner;
    }


    public override void EnterState()
    {
        capsuleCollider = owner.GetComponent<CapsuleCollider>();
        generalFunctions = owner.GetComponent<GeneralFunctions>();
    }
    protected void ApplyForce(Vector3 vector)
    {
        Velocity += vector;

    }

    protected void Gravity()
    {
        Vector3 gravity = Vector3.down * gravityConstant * Time.deltaTime;
        Velocity += gravity;

    }
    protected Vector3 GetDirectionInput()
    {
        Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));

        // Move in camera's direction
        input = Camera.main.transform.rotation * input;
        input = Vector3.ProjectOnPlane(input, GroundCast().normal).normalized;
        ChangeCharRotation();
        return input;


    }



    private void ChangeCharRotation()
    {
        Vector3 target ;
        target.x = 0;
        target.y = Camera.main.transform.rotation.y;
        target.z = 0;
        owner.transform.eulerAngles = target;
    }

    protected Vector3 LookDirection()
    {
        return -Camera.main.GetComponent<CameraScript>().getRelationship();
    }

   
    private RaycastHit GroundCast()
    {
        RaycastHit raycastHit;

        bool capsuleCast = Physics.CapsuleCast(owner.transform.position + capsuleCollider.center + Vector3.up * (capsuleCollider.height / 2 - capsuleCollider.radius), owner.transform.position + capsuleCollider.center + Vector3.down * (capsuleCollider.height / 2 - capsuleCollider.radius),
            capsuleCollider.radius, Vector3.down, out raycastHit, groundCheckDistance + skinWidth, owner.environment);

        return raycastHit;
    }

    protected bool IsGrounded()
    {
        Vector3 point1 = owner.transform.position + capsuleCollider.center + Vector3.up * (capsuleCollider.height / 2 - capsuleCollider.radius);
        Vector3 point2 = owner.transform.position + capsuleCollider.center + Vector3.down * (capsuleCollider.height / 2 - capsuleCollider.radius);
        RaycastHit raycastHit;
        bool capsuleCast = Physics.CapsuleCast(point1, point2, capsuleCollider.radius, Vector3.down, out raycastHit, groundCheckDistance + skinWidth, owner.environment);
        return capsuleCast;
    }

    protected void Accelerate(Vector3 direction)
    {

        if (direction.magnitude > 1)
        {

            direction = direction.normalized;
        }
        direction.y = 0;
        Velocity += direction * acceleration * Time.deltaTime;

        if (Velocity.magnitude > MaxSpeed)
        {
            Vector3 temp = Velocity.normalized;
            temp.y = 0;
            temp = temp.normalized * MaxSpeed;
            temp.y = Velocity.y;
            Velocity = temp;
        }

    }
    protected void Decelerate()
    {
        Vector3 tempVel = new Vector3(Velocity.x, 0, Velocity.z);
        Velocity -= tempVel.normalized * deceleration * Time.deltaTime;
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

    protected RaycastHit GetRaycast()
    {
        Vector3 point1 = owner.transform.position + capsuleCollider.center + Vector3.up * (capsuleCollider.height / 2 - capsuleCollider.radius);
        Vector3 point2 = owner.transform.position + capsuleCollider.center + Vector3.down * (capsuleCollider.height / 2 - capsuleCollider.radius);
        RaycastHit raycastHit;
        bool capsulecast = Physics.CapsuleCast(point1, point2,
            capsuleCollider.radius, Vector3.down, out raycastHit, Velocity.magnitude * Time.deltaTime + skinWidth, owner.environment);
        return raycastHit;
    }


    protected void CollisionCheck()
    {




        if (owner.transform.position.y < -2.6f)
        {
            SceneManager.LoadScene("test");
        }

        //Debug.Log("speed:" + MaxSpeed + " dynamic " + dynamicFriction);





        #region Raycast
        Vector3 point1 = owner.transform.position + capsuleCollider.center + Vector3.up * (capsuleCollider.height / 2 - capsuleCollider.radius);
        Vector3 point2 = owner.transform.position + capsuleCollider.center + Vector3.down * (capsuleCollider.height / 2 - capsuleCollider.radius);
        RaycastHit raycastHit;
        bool capsulecast = Physics.CapsuleCast(point1, point2,
            capsuleCollider.radius, Velocity, out raycastHit, Velocity.magnitude * Time.deltaTime + skinWidth, owner.environment);
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
    }

    protected bool isSnowboarding()
    {
        #region Raycast
        Vector3 point1 = owner.transform.position + capsuleCollider.center + Vector3.up * (capsuleCollider.height / 2 - capsuleCollider.radius);
        Vector3 point2 = owner.transform.position + capsuleCollider.center + Vector3.down * (capsuleCollider.height / 2 - capsuleCollider.radius);
        RaycastHit raycastHit;
        bool capsulecast = Physics.CapsuleCast(point1, point2, capsuleCollider.radius, Vector3.down, out raycastHit, Velocity.magnitude * Time.deltaTime, owner.pickups);
        #endregion

        if(raycastHit.collider != null)
        {
            //snowboarding = true;
            return true;
            
        }
        return false;
    }
    protected bool IsSnowboarding()
    {
        Vector3 point1 = owner.transform.position + capsuleCollider.center + Vector3.up * (capsuleCollider.height / 2 - capsuleCollider.radius);
        Vector3 point2 = owner.transform.position + capsuleCollider.center + Vector3.down * (capsuleCollider.height / 2 - capsuleCollider.radius);
        RaycastHit raycastHit;
        bool capsuleCast = Physics.CapsuleCast(point1, point2, capsuleCollider.radius, Vector3.down, out raycastHit, groundCheckDistance + skinWidth, owner.pickups);
        return capsuleCast;
    }

    protected GameObject TakingLift()
    {
        #region Raycast
        Vector3 point1 = owner.transform.position + capsuleCollider.center + Vector3.up * (capsuleCollider.height / 2 - capsuleCollider.radius);
        Vector3 point2 = owner.transform.position + capsuleCollider.center + Vector3.down * (capsuleCollider.height / 2 - capsuleCollider.radius);
        RaycastHit raycastHit;
        bool capsulecast = Physics.CapsuleCast(point1, point2, capsuleCollider.radius, Vector3.down, out raycastHit, groundCheckDistance + skinWidth, owner.lift);
        #endregion

        if (raycastHit.collider != null)
        {
            GameObject lift = raycastHit.transform.gameObject;
            return lift;

        }
        return null;
    }
}
