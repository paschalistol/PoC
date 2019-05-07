//Main Author: Emil Dahl
//Secondary Author: Paschalis Tolios

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/DogChaseState")]
public class DogChaseState : DogBaseState
{

  //  private float chaseDistance;
  //  private float hearingRange;
    private float smellDistance;
    [SerializeField] private float bustedDistance;
    private GameObject audioSpeaker;

    public override void EnterState()
    {
        base.EnterState();
        // hearingRange = owner.GetHearingDistance();
        // chaseDistance = owner.GetFieldOfView();
        smellDistance = owner.GetSmellDistance();
     //   audioSpeaker = owner.audioSpeaker;
            //ChaseEvent chaseEvent = new ChaseEvent();
            //chaseEvent.gameObject = owner.gameObject;
            //chaseEvent.eventDescription = "Chasing Enemy";
            //chaseEvent.audioSpeaker = audioSpeaker;

            //EventSystem.Current.FireEvent(chaseEvent);
        

    }
    public override void ToDo()
    {
        if (Vector3.Distance(owner.transform.position, owner.player.transform.position) < smellDistance)
        {

            owner.agent.SetDestination(owner.player.transform.position);

            if (Vector3.Distance(owner.transform.position, owner.player.transform.position) < bustedDistance)
                owner.ChangeState<DogDetectionState>();
        }  else
            owner.ChangeState<DogPatrolState>();

    }
}
