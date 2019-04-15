using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Character/SnowboardState")]
public class SnowboardState : CharacterBaseState
{

    GameObject snowboard;
    BoxCollider snowboardsCollider;
            
    public override void EnterState()
    {
        base.EnterState();
       // dynamicFriction = 2f;
        MaxSpeed = 30;
        snowboard = ReturnObjectInFront();
        snowboardsCollider = snowboard.GetComponent<BoxCollider>();
        dynamicFriction = 0.1f;
    }

    public override void ToDo()
    {

        #region Input
        Vector3 input = GetDirectionInput();

        
        
        Accelerate(input);
        #endregion
        Gravity();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ApplyForce(new Vector3(0, jumpHeight, 0));
        }
        CollisionCheck();

        owner.transform.position += Velocity * Time.deltaTime;
        

        if (!IsGrounded())
        {
            owner.ChangeState<InTheAirState>();

        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            owner.ChangeState<GroundedState>();
        }

        if (!IsSnowboarding())
        {

            owner.ChangeState<GroundedState>();
        }
        if (snowboard != null)
        {
            TransformCarriedObject();
        }
    }

    private void TransformCarriedObject()
    {
        Vector3 target;
        target.x = 0;
        target.y = Camera.main.transform.eulerAngles.y;
        target.z = 0;
        snowboard.transform.eulerAngles = target;
        owner.transform.position += new Vector3(0,snowboardsCollider.transform.localScale.y,0);
        //snowboard.GetComponent<Snowboard>().SetVelocity(new Vector3(Velocity.x, 0, Velocity.z));
       snowboard.transform.position = test();
    }
    private Vector3 test()
    {
        float x = owner.transform.position.x;
        float y = snowboard.transform.position.y;
      //  float y = owner.transform.position.y - capsuleCollider.height/2 - snowboardsCollider.transform.localScale.y/2 ;
        float z = owner.transform.position.z;
        return new Vector3(x, y, z);
    }
    private Vector3 Direction()
    {
        Vector3 project = Vector3.ProjectOnPlane(LookDirection(), Vector3.down).normalized;
        float x = owner.transform.position.x + project.x * (capsuleCollider.radius + snowboard.GetComponent<BoxCollider>().transform.localScale.x / 2);
        float y = snowboard.transform.localScale.y / 2 + capsuleCollider.height / 2 + owner.transform.position.y;
        float z = owner.transform.position.z + project.z * (capsuleCollider.radius + snowboard.GetComponent<BoxCollider>().transform.localScale.z / 2);
        return new Vector3(x, y, z);
    }
    protected GameObject ReturnObjectInFront()
    {
        Vector3 look = LookDirection();
        Vector3 point1 = owner.transform.position + capsuleCollider.center + Vector3.up * (capsuleCollider.height / 2 - capsuleCollider.radius);
        Vector3 point2 = owner.transform.position + capsuleCollider.center + Vector3.down * (capsuleCollider.height / 2 - capsuleCollider.radius);
        RaycastHit raycastHit;
        bool capsuleCast = Physics.CapsuleCast(point1, point2, capsuleCollider.radius, Vector3.down, out raycastHit, capsuleCollider.radius, owner.pickups);

        if (raycastHit.collider != null)
        {
            snowboard = raycastHit.transform.gameObject;
            return snowboard;
        }
        return null;
    }
    
    //private void TransformCarriedObject()
    //{
    //    RaycastHit raycastHit;
    //    bool boxCast = Physics.BoxCast(snowboard.transform.position, snowboard.transform.localScale, Vector3.down, out raycastHit, snowboard.transform.rotation, snowboard.transform.localScale.y + 0.003f, owner.environment);



    //    Vector3 target;
    //    /*   target.x = owner.transform.eulerAngles.x;
    //       target.y = Camera.main.transform.eulerAngles.y;
    //       target.z = owner.transform.eulerAngles.z;
    //       snowboard.transform.eulerAngles = target;*/
    //    owner.transform.position += new Vector3(raycastHit.distance, snowboardsCollider.transform.localScale.y, raycastHit.distance);
    //    // snowboard.transform.position = test();
    //}
    //private Vector3 test()
    //{
    //    Vector3 input = GetDirectionInput();
    //    input.y = snowboard.transform.position.y;
    //    input.x = owner.transform.position.x;
    //    //float y = snowboard.transform.position.y;
    //    //  float y = owner.transform.position.y - capsuleCollider.height/2 - snowboardsCollider.transform.localScale.y/2 ;
    //    input.z = owner.transform.position.z;
    //    return input;
    //}

}
