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
    private UnitDeathEventInfo deathInfo;
    private Vector3 heading;
    private float lightTreshold, dotProduct;
    private const float rotationalSpeed = 0.035f;
    protected float lightField, fieldOfView, hearingRange;
    protected const float soundFromFeet = 5f;
    protected const float investigationDistance = 15f;

    protected Enemy owner;


    // Methods
    public override void EnterState()
    {
        base.EnterState();
        owner.Renderer.material = material;
        owner.agent.speed = moveSpeed;
        capsuleCollider = owner.GetComponent<CapsuleCollider>();
        lightField = owner.flashLight.GetComponent<Light>().range;
        lightTreshold = 0.5f;
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

    protected void FetchDogs()
    {
        foreach (GameObject dog in owner.dogs)
        {
            dog.GetComponent<EnemyDog>().ChangeState<DogFetchState>();
        }
    }

    protected void KillPlayer()
    {
        deathInfo = new UnitDeathEventInfo();
        deathInfo.eventDescription = "You have been killed!";
        deathInfo.spawnPoint = owner.player.GetComponent<CharacterStateMachine>().currentCheckPoint;
        deathInfo.deadUnit = owner.player.transform.gameObject;
        EventSystem.Current.FireEvent(deathInfo);
    }

    protected void ScornDogs()
    {
        foreach (GameObject dog in owner.dogs)
        {
            dog.GetComponent<EnemyDog>().ChangeState<DogPatrolState>();
        }
    }

    protected void RotateEnemy()
    {
        Vector3 direction = owner.player.transform.position - owner.transform.position;
        Quaternion rotation = Quaternion.LookRotation(direction);
        owner.transform.rotation = Quaternion.Lerp(owner.transform.rotation, rotation, rotationalSpeed);
    }

    protected bool MakingSoundCheck(float distanceToPlayer)
    {
        if (InRangeCheck(distanceToPlayer) && moveSpeed > soundFromFeet && Input.anyKeyDown)
            return true;
        return false;
    }

    protected bool InRangeCheck(float distanceToPlayer)
    {
        if (distanceToPlayer < hearingRange)
            return true;
        return false;
    }
}
#region EnemyBaseLegacy
// lightTreshold = owner.LightThreshold;
//     spreadAngle = Quaternion.AngleAxis(lightField.spotAngle, owner.agent.velocity);
//// protected float lightAngle;
// //private Quaternion spreadAngle;
#endregion