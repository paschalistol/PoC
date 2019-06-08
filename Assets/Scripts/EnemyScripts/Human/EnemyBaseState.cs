//Main Author: Emil Dahl

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class holds most variables and methods for enemy behaviors 
/// </summary>
[RequireComponent(typeof(CapsuleCollider))]
public class EnemyBaseState : State
{

    #region SerializedVariables
    [SerializeField] protected Material material;
    [SerializeField] protected float moveSpeed;
    #endregion

    #region PrivateVariables
    private CapsuleCollider capsuleCollider;
    private UnitDeathEventInfo deathInfo;
    private Vector3 direction;
    private Quaternion rotation;
    private Vector3 heading;
    private float lightTreshold, dotProduct;
    #endregion

    #region ProtectedVariables
    protected Enemy owner;
    protected float lightField, fieldOfView, hearingRange;
    #endregion

    #region ConstantVariables;
    private const float rotationalSpeed = 0.035f;
    protected const float soundFromFeet = 5f;
    protected const float investigationDistance = 15f;
    #endregion

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

    /// <summary>
    /// Fires death event that handles player death 
    /// </summary>
    protected void KillPlayer()
    {
        deathInfo = new UnitDeathEventInfo();
        deathInfo.eventDescription = "You have been killed!";
        deathInfo.spawnPoint = owner.player.GetComponent<CharacterStateMachine>().currentCheckPoint;
        deathInfo.deadUnit = owner.player.transform.gameObject;
        EventSystem.Current.FireEvent(deathInfo);
    }

    #region Calculations 
    /// <summary>
    /// Used to calculate the angle at which the enemy should detect the player
    /// </summary>
    /// <returns>float</returns>
    protected float DotMethod()
    {
        heading = (owner.player.transform.position - owner.transform.position).normalized;
        dotProduct = Vector3.Dot(owner.agent.velocity.normalized, heading);
        return dotProduct;
    }

    /// <summary>
    /// Rotates the enemy based on direction of the player
    /// </summary>
    protected void RotateEnemy()
    {
        direction = owner.player.transform.position - owner.transform.position;
        rotation = Quaternion.LookRotation(direction);
        owner.transform.rotation = Quaternion.Lerp(owner.transform.rotation, rotation, rotationalSpeed);
    }
    #endregion

    #region HandleDog

    /// <summary>
    /// Makes all dogs connected to owner attack the player
    /// </summary>
    protected void FetchDogs()
    {
        foreach (GameObject dog in owner.dogs)
        {
            dog.GetComponent<EnemyDog>().ChangeState<DogFetchState>();
        }
    }

    /// <summary>
    /// Makes all dogs connected to owner go back to patrolling
    /// </summary>
    protected void ScornDogs()
    {
        foreach (GameObject dog in owner.dogs)
        {
            dog.GetComponent<EnemyDog>().ChangeState<DogPatrolState>();
        }
    }

    #endregion

    #region EnemySenses
    /// <summary>
    /// Checks if the enemy can see the player, is there something in the field of view - or is it blocked?
    /// </summary>
    /// <returns>bool</returns>
    protected bool LineOfSight()
    {
        bool lineCast = Physics.Linecast(owner.agent.transform.position, owner.player.transform.position, owner.visionMask);
        if (lineCast)
            return false;

        if (DotMethod() > lightTreshold && Vector3.Distance(owner.agent.transform.position, owner.player.transform.position) < lightField)
            return true;
        return false;
    }

/// <summary>
/// Checks if the enemy can hear the player, is the player moving to fast, or pressing buttons?
/// </summary>
/// <param name="distanceToPlayer"></param>
/// <returns></returns>
    protected bool MakingSoundCheck(float distanceToPlayer)
    {
        if (owner.player.GetComponent<CharacterStateMachine>().maxSpeed > soundFromFeet && Input.anyKeyDown)
            return true;
        return false;
    }

    /// <summary>
    /// Checks if the enemy is in range of the player
    /// </summary>
    /// <param name="distanceToPlayer"></param>
    /// <returns></returns>
    protected bool InRangeCheck(float distanceToPlayer)
    {
        if (distanceToPlayer < hearingRange)
            return true;
        return false;
    }
    #endregion

}
#region EnemyBaseLegacy
// lightTreshold = owner.LightThreshold;
//     spreadAngle = Quaternion.AngleAxis(lightField.spotAngle, owner.agent.velocity);
//// protected float lightAngle;
// //private Quaternion spreadAngle;
#endregion