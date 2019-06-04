//Main Author: Paschalis Tolios
//Secondary authors: Emil Dahl, Johan Ekman

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterBaseState : State
{
    protected CharacterStateMachine owner;

    
    protected float wobbleValue { get { return owner.WobbleFactor; } }
    protected CapsuleCollider capsuleCollider;
    protected const int acceleration = 100;
    protected const float skinWidth = 0.1f;
    protected const float gravityConstant = 20f;
    protected const float groundCheckDistance = 0.25f;
    protected Vector3 Velocity { get { return owner.velocity; } set { owner.velocity = value; } }
    protected float MaxSpeed { get { return owner.maxSpeed; } set { owner.maxSpeed = value; } }
    protected const float deceleration = 50;
    protected Vector2 airResistance;
    protected const float staticFriction = 0.55f;
    protected float dynamicFriction;
    protected const float airCoeff = 0.4f;
    protected Vector3 normal;
    protected const float jumpHeight = 12;
    protected bool snowboarding = false;
    private float normalOffset = 0.03f;
    private Vector3 point1, point2;
    private RaycastHit raycastHit;
  
    

    public override void InitializeState(StateMachine owner)
    {
        this.owner = (CharacterStateMachine)owner;
    }


    public override void EnterState()
    {
        capsuleCollider = owner.GetComponent<CapsuleCollider>();

        
    }
    protected void ApplyForce(Vector3 vector)
    {
        Velocity = vector;

    }

    protected void Bouncing()
    {
        if (owner.standOnTrampoline == true)
        {

            ApplyForce(new Vector3(Velocity.x * 1.18f, owner.bounceHeight, Velocity.z * 1.18f));
        }
        owner.standOnTrampoline = false;
    }

    protected void Gravity()
    {
        Vector3 gravity = Vector3.down * gravityConstant * Time.deltaTime;
        Velocity += gravity;

    }
    protected Vector3 GetDirectionInput()
    {
        float rndWobble = Random.Range(0.0f, 1.0f);


        Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));

        if (rndWobble < wobbleValue && input != Vector3.zero)
        {
            float rndDirection = Random.Range(0.0f, 1.0f);


            if (rndDirection < 0.5)
            {
                input = Quaternion.Euler(0, -90, 0) * input;

            }
            else if (rndDirection > 0.5)
            {
                input = Quaternion.Euler(0, 90, 0) * input;

            }

        }


        // Move in camera's direction
        input = Camera.main.transform.rotation * input;
        input = Vector3.ProjectOnPlane(input, GetRaycastHit(Vector3.down, groundCheckDistance + skinWidth, owner.environment).normal).normalized;
        // ChangeCharRotation();
        return input;
    }


    protected void ChangeCharRotation()
    {
        Vector3 target = Vector3.zero;
        target.y = Camera.main.transform.eulerAngles.y;

        owner.transform.eulerAngles = target;
    }


    protected Vector3 LookDirection()
    {
        return -Camera.main.GetComponent<CameraScript>().getRelationship();
    }



    protected bool IsGliding()
    {
        bool capsulecast = Physics.CapsuleCast(point1, point2,
            capsuleCollider.radius, Vector3.down, out raycastHit, groundCheckDistance + skinWidth, owner.environment);

        return (raycastHit.normal.y < 1 - normalOffset || raycastHit.normal.y > 1 + normalOffset) && raycastHit.normal.y != 0 && raycastHit.collider.gameObject.layer == 9;
    }

    protected bool IsGrounded()
    {

        return GetRaycastHit(Vector3.down, groundCheckDistance + skinWidth, owner.environment).collider != null;
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
            Vector3 temp = Velocity;
            temp.y = 0;
            temp = temp.normalized * MaxSpeed;
            temp.y = Velocity.y;
            Velocity = temp;
        }

    }
    Vector3 tempVel;
    protected void Decelerate()
    {
        tempVel = new Vector3(Velocity.x, 0, Velocity.z);
        if ((tempVel.normalized * deceleration * Time.deltaTime).magnitude >Velocity.magnitude)
        {

        Velocity -= tempVel.normalized * deceleration * Time.deltaTime;
        }
        else
        {
            Velocity = new Vector3(0, Velocity.y, 0);
        }
    }
    private void Friction(float normalMag)
    {
        if (Velocity.magnitude < (staticFriction * normalMag))
        {
            Velocity = Vector3.zero;
        }
        else
        {
            Velocity += (dynamicFriction * normalMag) * -Velocity.normalized;
        }
    }

    private RaycastHit GetRaycastHit(Vector3 direction, float magnitude, LayerMask layerMask)
    {
        Debug.DrawRay(point1, owner.transform.up, Color.red);
        Debug.DrawRay(point2, -owner.transform.up, Color.blue);
        point1 = owner.transform.position + capsuleCollider.center + owner.transform.up * (capsuleCollider.height / 2 - capsuleCollider.radius);
        point2 = owner.transform.position + capsuleCollider.center - owner.transform.up * (capsuleCollider.height / 2 - capsuleCollider.radius);
        Physics.CapsuleCast(point1, point2,
            capsuleCollider.radius, direction, out raycastHit, magnitude, layerMask);
        return raycastHit;
    }


    float tempSkinwidth, angle, h;
    public void CollisionCheck()
    {

        if (GetRaycastHit(Velocity, Mathf.Infinity, owner.environment).collider != null)

        {

            angle = (Vector3.Angle(raycastHit.normal, Velocity.normalized) - 90) * Mathf.Deg2Rad;
            h = skinWidth / Mathf.Sin(angle);

                tempSkinwidth = (skinWidth > h ? skinWidth : h);
            if (Velocity.magnitude * Time.deltaTime + tempSkinwidth > raycastHit.distance - tempSkinwidth)
            {

                normal = PhysicsScript.Normal3D(Velocity, raycastHit.normal);
                Velocity += normal;
                Friction(normal.magnitude);

                if (Velocity.magnitude < tempSkinwidth)
                {
                    return;
                }
                CollisionCheck();
            }
        }
    }

    //protected void CollisionCheck()
    //{
    //    GetRaycastHit(Velocity, Velocity.magnitude * Time.deltaTime + skinWidth, owner.environment);



    //    if (raycastHit.collider == null)
    //        return;
    //    else
    //    {

    //        #region Apply Normal Force
    //        normal = PhysicsScript.Normal3D(Velocity, raycastHit.normal);
    //        Velocity += normal;
    //        Friction(normal.magnitude);
    //        #endregion
    //        if (Velocity.magnitude < skinWidth)
    //        {
    //            Velocity = Vector3.zero;
    //            return;
    //        }

    //        CollisionCheck();
    //    }

    //}


    //float tempSkinwidth;
    //protected void CollisionCheck()
    //{
    //    GetRaycastHit(Velocity, Mathf.Infinity, owner.environment);


    //    if (raycastHit.collider != null)
    //    {
    //        #region H
    //        float angle = Vector3.Angle(Velocity.normalized, raycastHit.normal) - 90;
    //        Debug.Log(Velocity + " " + angle);
    //        float sin = Mathf.Sin(angle);
    //        float h = skinWidth / sin;
    //        tempSkinwidth = (skinWidth > h ? skinWidth : h);
    //        #endregion
    //        if (Velocity.magnitude * Time.deltaTime + tempSkinwidth > raycastHit.distance - tempSkinwidth)
    //        {



    //            if (Velocity.magnitude < tempSkinwidth)
    //            {
    //                Velocity = Vector3.zero;
    //                return;
    //            }
    //            #region Apply Normal Force
    //            normal = PhysicsScript.Normal3D(Velocity, raycastHit.normal);
    //            Velocity += normal;
    //            Friction(normal.magnitude);
    //            #endregion

    //            CollisionCheck();
    //        }

    //    }

    //}




    protected void DeathCollisionCheck()
    {

        GetRaycastHit(Vector3.down, Velocity.magnitude * Time.deltaTime + skinWidth * 2f, owner.deadlyEnvironment);

        if (raycastHit.collider != null)
        {

            UnitDeathEventInfo deathInfo = new UnitDeathEventInfo();
            deathInfo.eventDescription = "You are dead";
            deathInfo.spawnPoint = owner.currentCheckPoint;

            deathInfo.deadUnit = owner.transform.gameObject;
            EventSystem.Current.FireEvent(deathInfo);

        }
        if (DeathListener.Death())
        {
            //owner.transform.position = owner.currentCheckPoint.transform.position;
            HoldingItem.SetDied();
            DeathListener.SetDied(false);
        }

    }


    protected GameObject TakingLift2()
    {
        GetRaycastHit(Vector3.down, groundCheckDistance + skinWidth, owner.lift);
        if (raycastHit.collider != null)
        {
            owner.lift2 = raycastHit.transform.gameObject;
            return owner.lift2;

        }
        return null;
    }

    protected void ReachingCheckPoint()
    {

        //GetRaycastHit(Velocity, Velocity.magnitude * Time.deltaTime + skinWidth, owner.checkPoint);
        //if (raycastHit.collider != null)
        //    owner.currentCheckPoint = raycastHit.collider.gameObject;
    }
    

    

     /*protected void ReachingGoal()
     {
         
        GetRaycastHit(Velocity, Velocity.magnitude * Time.deltaTime + skinWidth * 2f, owner.goal);

         if (raycastHit.collider != null)
         {

             WinningEvent winInfo = new WinningEvent();
             winInfo.eventDescription = "You won!";

             winInfo.gameObject = owner.transform.gameObject;
             EventSystem.Current.FireEvent(winInfo);

         }
     }*/
     
}
