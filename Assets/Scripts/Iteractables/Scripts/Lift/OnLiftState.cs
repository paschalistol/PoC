using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Lift/OnLiftState")]
public class OnLiftState : GroundedState
{


    

    public void Awake()
    {
 
    }

    public override void EnterState()
    {
        base.EnterState();
        
        
    }

    public override void ToDo()
    {

       // Velocity = owner.lift2.GetComponent<Lift2>().GetVelocity();
        #region Input
        Vector3 input = GetDirectionInput();


        if (input.magnitude <= 0)
        {
           
        }
        else
        {
            Accelerate(input);
        }
        #endregion
        Debug.Log(Velocity.z);

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

        owner.transform.position += (owner.lift2.GetComponent<Lift2>().GetVelocity() +Velocity) *Time.deltaTime;


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
