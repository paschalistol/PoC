using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Character/InTheAir")]
public class InTheAirState : CharacterBaseState
{
    public override void EnterState()
    {
        base.EnterState();
        Debug.Log(Velocity);
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
        CollisionCheck();
        DeathCollisionCheck();
        ReachingCheckPoint();
        Trampoline();
        owner.transform.position += Velocity * Time.deltaTime;
        if (IsGrounded())
        {

            owner.ChangeState<GroundedState>();
        }
        else if (TakingLift2())
        {
            owner.ChangeState<OnLiftState>();
        }
    }

}
