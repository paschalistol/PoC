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
    }

    public override void ToDo()
    {
        if (!GameController.isPaused)
        {
            ChangeCharRotation();
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

            CollisionCheck();

            owner.transform.position += (owner.lift2.GetComponent<Lift2>().GetVelocity() + Velocity) * Time.deltaTime;


            if (!IsGrounded())
            {
                owner.ChangeState<InTheAirState>();

            }
            else if (IsGrounded() && !TakingLift2())
            {

                owner.ChangeState<GroundedState>();
            }
            // owner.transform.parent = owner.lift2.transform;
        }
    }
}
