//Main Author: Emil Dahl
//Secondary Author: Paschalis Tolios

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class handles enemy chase behavior
/// </summary>
[CreateAssetMenu(menuName = "Enemy/ChaseState")]
public class ChaseState : EnemyBaseState
{

    [SerializeField] private float lightIntensity = 15;
    private MusicBasedOnChased musicBasedOnChased;
    private UnitDeathEventInfo deathInfo;
    private GameObject audioSpeaker;
    private float chaseDistance, distanceToPlayer, lightRange, movementSpeed;
    private const float bustedDistance = 2f;

    public override void EnterState()
    {
        base.EnterState();
        chaseDistance = owner.GetFieldOfView();
        lightRange = owner.flashLight.GetComponent<Light>().range;
        owner.flashLight.GetComponent<Light>().intensity = lightIntensity;
        owner.flashLight.GetComponent<Light>().color = Color.red;

        musicBasedOnChased = new MusicBasedOnChased();
        musicBasedOnChased.enemyChasing = true;
        EventSystem.Current.FireEvent(musicBasedOnChased);

        EventSystem.Current.RegisterListener<UnitDeathEventInfo>(HandleDeath);
    }

    public override void ToDo()
    {

        if (!GameController.isPaused)
        {
            distanceToPlayer = Vector3.Distance(owner.transform.position, owner.player.transform.position);
            movementSpeed = owner.player.GetComponent<CharacterStateMachine>().GetMaxSpeed();

            owner.agent.SetDestination(owner.player.transform.position);
            FetchDogs();

            #region IdleEnemy
            if (!owner.agent.hasPath)
            {
                RotateEnemy();
                owner.agent.isStopped = true;
            }
            else
            {
                owner.agent.isStopped = false;
            }
            #endregion

            if (distanceToPlayer < bustedDistance)
            {
                KillPlayer();
            }

            #region PatrolBehaviors

            if (!LineOfSight() && (InRangeCheck(distanceToPlayer) && MakingSoundCheck(distanceToPlayer) && distanceToPlayer > investigationDistance))
            {

                owner.ChangeState<InvestigationState>();
                ScornDogs();
            }
            else if (!InRangeCheck(distanceToPlayer) && !GameController.activatedAlarm)
                owner.ChangeState<PatrolState>();
            #endregion
        }
        else { owner.agent.SetDestination(owner.agent.transform.position); }
    }

    /// <summary>
    /// If the player dies the enemy returns to patrolling, his dogs does the same and the alarm is shut off
    /// </summary>
    /// <param name="death"></param>
    void HandleDeath(UnitDeathEventInfo death)
    {
        ScornDogs();
        GameController.activatedAlarm = false;
        owner.ChangeState<PatrolState>();
    }

    public override void ExitState()
    {
        musicBasedOnChased = new MusicBasedOnChased();
        musicBasedOnChased.enemyChasing = false;
        EventSystem.Current.FireEvent(musicBasedOnChased);
    }
}
#region ChaseLegacy
// lightAngle = lightField.spotAngle;
//ChaseEvent chaseEvent = new ChaseEvent();
//chaseEvent.gameObject = owner.gameObject;
//chaseEvent.eventDescription = "Chasing Enemy";
//chaseEvent.audioSpeaker = audioSpeaker;

//EventSystem.Current.FireEvent(chaseEvent);
#endregion