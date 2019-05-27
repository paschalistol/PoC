//Main Author: Emil Dahl
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldboiBaseState : State
{
    // Attributes
    [SerializeField] protected float moveSpeed;
    [SerializeField] protected Material material;
    private float fieldOfView;
    private CapsuleCollider capsuleCollider;
    private UnitDeathEventInfo deathInfo;

    protected Oldboi owner;
   
    // Methods
    public override void EnterState()
    {
        base.EnterState();
        owner.Renderer.material = material;
        owner.agent.speed = moveSpeed;
        fieldOfView = owner.GetFieldOfView();
        capsuleCollider = owner.GetComponent<CapsuleCollider>();
    }
 
    public override void InitializeState(StateMachine owner)
    {
        this.owner = (Oldboi)owner;
    }

    protected bool LineOfSight()
    {
        //Debug.Log(owner.agent.velocity.normalized);
        Debug.DrawRay(owner.agent.transform.position, owner.agent.velocity, Color.red, 0);
        return Physics.CapsuleCast(owner.transform.position + capsuleCollider.center + Vector3.up * (capsuleCollider.height / 2 - capsuleCollider.radius), owner.transform.position + capsuleCollider.center + Vector3.down * (capsuleCollider.height / 2 - capsuleCollider.radius),
           capsuleCollider.radius, owner.agent.velocity, out RaycastHit raycastHit, fieldOfView, owner.visionMask);
    }

    protected void FetchDogs()
    {
        foreach (GameObject dog in owner.dogs)
        {
            dog.GetComponent<EnemyDog>().ChangeState<DogFetchState>();
        }
    }

    protected void ScornDogs()
    {
        foreach (GameObject dog in owner.dogs)
        {
            dog.GetComponent<EnemyDog>().ChangeState<DogPatrolState>();
        }
    }
}
