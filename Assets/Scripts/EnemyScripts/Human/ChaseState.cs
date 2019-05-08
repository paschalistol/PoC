//Main Author: Emil Dahl
//Secondary Author: Paschalis Tolios

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/ChaseState")]
public class ChaseState : EnemyBaseState
{

    private float chaseDistance;
    private float hearingRange;
    [SerializeField] private float bustedDistance;
    private float lightRange;
    private GameObject audioSpeaker;

    public override void EnterState()
    {
        base.EnterState();
        hearingRange = owner.GetHearingDistance();
        chaseDistance = owner.GetFieldOfView();
        lightRange = owner.flashLight.GetComponent<Light>().range;
        owner.flashLight.GetComponent<Light>().intensity = 25;
        owner.flashLight.GetComponent<Light>().color = Color.red;
            //ChaseEvent chaseEvent = new ChaseEvent();
            //chaseEvent.gameObject = owner.gameObject;
            //chaseEvent.eventDescription = "Chasing Enemy";
            //chaseEvent.audioSpeaker = audioSpeaker;

            //EventSystem.Current.FireEvent(chaseEvent);
        

    }
    public override void ToDo()
    {

        fieldOfView = Vector3.Angle(owner.transform.position, owner.player.transform.position);
       // lightAngle = lightField.spotAngle;

        if ((LineOfSight() && Vector3.Distance(owner.transform.position, owner.player.transform.position) < lightRange) ||
            (Vector3.Distance(owner.transform.position, owner.player.transform.position) < hearingRange &&
            owner.player.GetComponent<CharacterStateMachine>().GetMaxSpeed() >= 5))
        {

            owner.agent.SetDestination(owner.player.transform.position);

            if (Vector3.Distance(owner.transform.position, owner.player.transform.position) < bustedDistance)
                owner.ChangeState<DetectionState>();
        }  else
            owner.ChangeState<PatrolState>(); 

    }
}
