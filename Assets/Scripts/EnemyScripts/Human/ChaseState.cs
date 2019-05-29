﻿//Main Author: Emil Dahl
//Secondary Author: Paschalis Tolios

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/ChaseState")]
public class ChaseState : EnemyBaseState
{

    private MusicBasedOnChased musicBasedOnChased;
    private UnitDeathEventInfo deathInfo;
    private GameObject audioSpeaker;
    private float chaseDistance, distanceToPlayer, lightRange, movementSpeed;
    private const float bustedDistance = 2f;


    public override void EnterState()
    {
        base.EnterState();
        //hearingRange = owner.GetHearingDistance();
        chaseDistance = owner.GetFieldOfView();
        lightRange = owner.flashLight.GetComponent<Light>().range;
        owner.flashLight.GetComponent<Light>().intensity = 25;
        owner.flashLight.GetComponent<Light>().color = Color.red;
        musicBasedOnChased = new MusicBasedOnChased();
        musicBasedOnChased.enemyChasing = true;
        EventSystem.Current.FireEvent(musicBasedOnChased);
    }
    public override void ToDo()
    {
        if (!GameController.isPaused)
        {
            fieldOfView = Vector3.Angle(owner.transform.position, owner.player.transform.position);
            distanceToPlayer = Vector3.Distance(owner.transform.position, owner.player.transform.position);
            movementSpeed = owner.player.GetComponent<CharacterStateMachine>().GetMaxSpeed();

            owner.agent.SetDestination(owner.player.transform.position);
            FetchDogs();

            if (!owner.agent.hasPath)
            {
                RotateEnemy();
                owner.agent.isStopped = true;
            }
            else
            {
                owner.agent.isStopped = false;
            }


            if (distanceToPlayer < bustedDistance)
            {
                KillPlayer();
                owner.ChangeState<PatrolState>();
            }
            if ((!LineOfSight() && ((distanceToPlayer < hearingRange && movementSpeed > soundFromFeet
                 && Input.anyKeyDown)) && distanceToPlayer > investigationDistance))
            {
                owner.ChangeState<InvestigationState>();
                ScornDogs();
            }


        }
        else { owner.agent.SetDestination(owner.agent.transform.position); }
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