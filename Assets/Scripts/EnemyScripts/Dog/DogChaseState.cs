﻿//Main Author: Emil Dahl
//Secondary Author: Paschalis Tolios

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/DogChaseState")]
public class DogChaseState : DogBaseState
{

    private float smellDistance;
    private const float bustedDistance = 2f;
    private UnitDeathEventInfo deathInfo;

    public override void EnterState()
    {
        base.EnterState();
        smellDistance = owner.GetSmellDistance();
    }
    /// <summary>
    /// Decides if the dog will return to patrol or attack while chasing the player.
    /// </summary>
    public override void ToDo()
    {
        if (!GameController.isPaused)
        {
            if (Vector3.Distance(owner.transform.position, owner.player.transform.position) >= smellDistance
                || owner.inSafeZone)
            {
                owner.ChangeState<DogPatrolState>();
            }
            else
            {
                owner.agent.SetDestination(owner.player.transform.position);
                if (Vector3.Distance(owner.transform.position, owner.player.transform.position) < bustedDistance)
                {
                    KillPlayer();
                    owner.ChangeState<DogPatrolState>();
                }
            }
        }else { owner.agent.SetDestination(owner.agent.transform.position); }
    }
}
#region ChaseLegacy
//public override void ExitState()
//{
//    Debug.Log("Walla");
//    owner.agent.transform.position = owner.agent.transform.position;
//    base.ExitState();
//}
//   audioSpeaker = owner.audioSpeaker;
//ChaseEvent chaseEvent = new ChaseEvent();
//chaseEvent.gameObject = owner.gameObject;
//chaseEvent.eventDescription = "Chasing Enemy";
//chaseEvent.audioSpeaker = audioSpeaker;
//private GameObject audioSpeaker;
//EventSystem.Current.FireEvent(chaseEvent);
// hearingRange = owner.GetHearingDistance();
// chaseDistance = owner.GetFieldOfView();
//  private float chaseDistance;
//  private float hearingRange;
#endregion
