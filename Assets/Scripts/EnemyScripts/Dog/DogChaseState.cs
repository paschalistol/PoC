//Main Author: Emil Dahl
//Secondary Author: Paschalis Tolios

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/DogChaseState")]
public class DogChaseState : DogBaseState
{

    private float smellDistance;
    [SerializeField] private float bustedDistance;
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
        if (Vector3.Distance(owner.transform.position, owner.player.transform.position) >= smellDistance || owner.inSafeZone)
        {
            owner.ChangeState<DogPatrolState>();
        }
        else
        {
            owner.agent.SetDestination(owner.player.transform.position);
            if (Vector3.Distance(owner.transform.position, owner.player.transform.position) < bustedDistance)
            {
                deathInfo = new UnitDeathEventInfo();
                deathInfo.eventDescription = "U big dead lmao!";
                deathInfo.spawnPoint = owner.player.GetComponent<CharacterStateMachine>().currentCheckPoint;
                deathInfo.deadUnit = owner.player.transform.gameObject;
                EventSystem.Current.FireEvent(deathInfo);

                owner.ChangeState<DogPatrolState>();
            }
        }
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
