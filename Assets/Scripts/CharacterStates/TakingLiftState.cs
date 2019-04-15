using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Character/TakingLiftState")]
public class TakingLiftState : CharacterBaseState
{

    public override void EnterState()
    {
        base.EnterState();
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
        if (Input.GetKeyDown(KeyCode.Q) && GetRaycast().normal != Vector3.up)
        {
            owner.ChangeState<GlidingState>();
        }

        CollisionCheck();
        owner.transform.position += Velocity * Time.deltaTime;


        GameObject lift = TakingLift();
        owner.transform.position += Vector3.up * 0.03f;
        owner.transform.parent = lift.transform;
        Debug.Log("hit");

        if (TakingLift() == null && IsGrounded())
        {
            owner.ChangeState<GroundedState>();
        } 
        if(TakingLift() == null && !IsGrounded())
        {
            owner.ChangeState<InTheAirState>();
        }

    }


}
