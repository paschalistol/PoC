﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Character/GroundedState")]
public class GroundedState : CharacterBaseState
{
    public override void EnterState()
    {
        base.EnterState();
        dynamicFriction = 0.35f;
        MaxSpeed = 10;
    }

    public override void ToDo()
    {

        #region Input
        Vector3 input = GetDirectionInput();


        if (input.magnitude <= 0)
        {
            Decelerate();
        }
        else
        {
            Accelerate(input);
        }
        #endregion
        Gravity();
        #region Buttons
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ApplyForce(new Vector3(0, jumpHeight, 0));
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            MaxSpeed /= 2;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            MaxSpeed *= 2;
        }
        #endregion
        if (GetRaycast().normal.y< 0.9f || GetRaycast().normal.y > 1.1f)
        {
            owner.ChangeState<GlidingState>();
        }

        CollisionCheck();
        DeathCollisionCheck();
        ReachingCheckPoint();
        owner.transform.position += Velocity * Time.deltaTime;



        if(TakingLift2() != null)
        {
            owner.ChangeState<OnLiftState>();
        }
        if (!IsGrounded())
        {
            owner.ChangeState<InTheAirState>();

        }
        //if (IsSnowboarding())
        //{

        //    owner.ChangeState<SnowboardState>();
        //}

    }


}
