using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Character/GlidingState")]
public class GlidingState : CharacterBaseState
{
    [SerializeField] private float dynamicWhenGliding = 0.1f, maxSpeed = 30;
    public override void EnterState()
    {
        base.EnterState();
        MaxSpeed = maxSpeed;
        dynamicFriction = dynamicWhenGliding;
    }

    public override void ToDo()
    {
        RotateToBelly();
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



        owner.transform.position += Velocity * Time.deltaTime;


        if (!IsGliding())
        {
            owner.ChangeState<GroundedState>();
        }
        if (!IsGrounded())
        {
            owner.ChangeState<InTheAirState>();

        }
    }


    private void RotateToBelly()
    {
        Vector3 point1 = owner.transform.position + capsuleCollider.center + owner.transform.up * (capsuleCollider.height / 2 - capsuleCollider.radius);
        Vector3 point2 = owner.transform.position + capsuleCollider.center - owner.transform.up * (capsuleCollider.height / 2 - capsuleCollider.radius);
        RaycastHit raycastHit;
        bool capsulecast = Physics.CapsuleCast(point1, point2,
            capsuleCollider.radius, Vector3.down, out raycastHit, groundCheckDistance + skinWidth, owner.environment);

        #region
        Vector3 temp = Velocity;
        if (temp.x == 0 && temp.z == 0)
        {
          //  temp.y = 0;
        }

        temp = temp.normalized * 90;
        #endregion

        Quaternion slopeRotation = Quaternion.FromToRotation(owner.transform.up, raycastHit.normal +temp);
        owner.transform.rotation = Quaternion.Slerp(owner.transform.rotation, slopeRotation * owner.transform.rotation, 1 * Time.deltaTime);

    }
}
