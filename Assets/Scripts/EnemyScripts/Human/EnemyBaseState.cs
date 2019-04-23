using System.Collections;

using System.Collections.Generic;
using UnityEngine;

public class EnemyBaseState : State
{
    // Attributes
    [SerializeField] protected float moveSpeed;
    [SerializeField] protected Material material;
    protected float fieldOfView;
    private CapsuleCollider capsuleCollider;
    protected Light lightField;
    //private Quaternion spreadAngle;
    protected float lightAngle;
    private float lightTreshold;

    protected Enemy owner;


    // Methods
    public override void EnterState()
    {
        base.EnterState();
        owner.Renderer.material = material;
        owner.agent.speed = moveSpeed;
        capsuleCollider = owner.GetComponent<CapsuleCollider>();
        lightField = owner.flashLight.GetComponent<Light>();
        lightTreshold = owner.LightThreshold;
       
        //spreadAngle = Quaternion.AngleAxis(lightField.spotAngle, owner.agent.velocity);
    }

    public override void InitializeState(StateMachine owner)
    {
        this.owner = (Enemy)owner;
    }

    protected bool LineOfSight()
    {
        //Vector3 tempDirection = Quaternion.Euler(180, 180, 180) * owner.agent.velocity;
        //Vector3 newVec = spreadAngle * owner.agent.velocity;
        //Debug.Log(owner.agent.velocity.normalized);
    
        if (DotMethod() > lightTreshold && Vector3.Distance(owner.agent.transform.position, owner.player.transform.position) < lightField.range)
        {
            //Debug.DrawRay(owner.agent.transform.position, owner.agent.velocity, Color.red, 0);
            //return Physics.CapsuleCast(owner.transform.position + capsuleCollider.center + Vector3.up * (capsuleCollider.height / 2 - capsuleCollider.radius), owner.transform.position + capsuleCollider.center + Vector3.down * (capsuleCollider.height / 2 - capsuleCollider.radius),
            //   capsuleCollider.radius, owner.agent.velocity, out RaycastHit raycastHit, lightField.range, owner.visionMask);


            return Physics.Linecast(owner.agent.transform.position, owner.player.transform.position);
        }
        else
            return false;
        
        
    }

    protected float DotMethod()
    {
        Vector3 heading = (owner.player.transform.position - owner.transform.position).normalized;
        float dotProduct = Vector3.Dot(owner.agent.velocity, heading);
        return dotProduct;
    }
}
