//Main Author: Emil Dahl

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
public class EnemyBaseState : State
{
    // Attributes
    [SerializeField] protected Material material;
    [SerializeField] protected float moveSpeed;
    private CapsuleCollider capsuleCollider;
    private Vector3 heading;
    private float lightTreshold, dotProduct;
    protected float lightField, fieldOfView, hearingRange;
    protected const float investigationDistance = 5f;


    protected Enemy owner;


    // Methods
    public override void EnterState()
    {
        base.EnterState();
        owner.Renderer.material = material;
        owner.agent.speed = moveSpeed;
        capsuleCollider = owner.GetComponent<CapsuleCollider>();
        lightField = owner.flashLight.GetComponent<Light>().range;
        lightTreshold = 0.65f;
        hearingRange = lightField * 1.5f;
    }

    public override void InitializeState(StateMachine owner)
    {
        this.owner = (Enemy)owner;
    }

    protected bool LineOfSight()
    {
        bool lineCast = Physics.Linecast(owner.agent.transform.position, owner.player.transform.position, owner.visionMask);
        if (lineCast)
            return false; 
      
        if (DotMethod() > lightTreshold && Vector3.Distance(owner.agent.transform.position, owner.player.transform.position) < lightField)
            return true;
        return false;
    }

    protected float DotMethod()
    {
        heading = (owner.player.transform.position - owner.transform.position).normalized;
        dotProduct = Vector3.Dot(owner.agent.velocity.normalized, heading);
        return dotProduct;
    }
}
#region EnemyBaseLegacy
        // lightTreshold = owner.LightThreshold;
//     spreadAngle = Quaternion.AngleAxis(lightField.spotAngle, owner.agent.velocity);
//// protected float lightAngle;
// //private Quaternion spreadAngle;
#endregion