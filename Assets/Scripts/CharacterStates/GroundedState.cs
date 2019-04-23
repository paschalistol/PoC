using System.Collections;
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
        owner.transform.parent = null;
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
        if (/*Input.GetKeyDown(KeyCode.Q) && */GetRaycast().normal != Vector3.up)
        {
            owner.ChangeState<GlidingState>();
        }

        CollisionCheck();
        owner.transform.position += Velocity * Time.deltaTime;

        if (TakingLift() != null)
        {

            owner.ChangeState<TakingLiftState>();
        }

        if(TakingLift2() != null)
        {
            owner.ChangeState<OnLiftState>();
        }
        if (!IsGrounded())
        {
            owner.ChangeState<InTheAirState>();

        }
        if (IsSnowboarding())
        {

            owner.ChangeState<SnowboardState>();
        }

    }


}
