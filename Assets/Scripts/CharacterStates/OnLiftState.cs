using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Lift/OnLiftState")]
public class OnLiftState : GroundedState
{
    public override void EnterState()
    {
        base.EnterState();

        owner.transform.position += new Vector3(0, skinWidth, 0);
        owner.grounded = true;
    }

    public override void ToDo()
    {
        if (!GameController.isPaused)
        {
            Basic();

            owner.transform.position += (owner.lift2.GetComponent<Lift2>().GetVelocity() + Velocity) * Time.deltaTime;


            if (!IsGrounded())
            {
                owner.ChangeState<InTheAirState>();

            }
            else if (IsGrounded() && !TakingLift2())
            {

                owner.ChangeState<GroundedState>();
            }
        }
    }
}
