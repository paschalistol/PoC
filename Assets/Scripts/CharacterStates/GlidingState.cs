using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Character/GlidingState")]
public class GlidingState : CharacterBaseState
{
    public override void EnterState()
    {
        base.EnterState();
        MaxSpeed = 30;
        dynamicFriction = 0.1f;
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
        owner.transform.position += Velocity * Time.deltaTime;
        if (/*Input.GetKeyDown(KeyCode.Q) || */ GetRaycast().normal == Vector3.up)
        {
            owner.ChangeState<GroundedState>();
        }
    }
}
