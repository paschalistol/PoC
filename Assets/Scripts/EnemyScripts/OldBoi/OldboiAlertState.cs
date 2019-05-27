//Main Author: Emil Dahl
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/OldboiAlertState")]
public class OldboiAlertState : OldboiBaseState
{
 
    private float chaseDistance;
    private float hearingRange;
    private const float bustedDistance = 2f;
    private const float rotationalSpeed = 0.1f;

    //public AudioClip audioSpeaker;

    public override void EnterState()
    {
        base.EnterState();
        hearingRange = owner.GetHearingDistance();
        chaseDistance = owner.GetFieldOfView();
    }
    public override void ToDo()
    {
        if (!GameController.isPaused)
        {
            Vector3 direction = owner.player.transform.position - owner.transform.position;
            Quaternion rotation = Quaternion.LookRotation(direction);
            owner.transform.rotation = Quaternion.Lerp(owner.transform.rotation, rotation, rotationalSpeed);

            if ((LineOfSight() && Vector3.Distance(owner.transform.position, owner.player.transform.position) < chaseDistance) ||
                (Vector3.Distance(owner.transform.position, owner.player.transform.position) < hearingRange &&
                owner.player.GetComponent<CharacterStateMachine>().GetMaxSpeed() >= 5))
            {
                foreach (GameObject dog in owner.dogs)
                {
                    dog.GetComponent<EnemyDog>().ChangeState<DogFetchState>();
                }
            }
            else
            {
                owner.ChangeState<OldboiPatrolState>();
                foreach (GameObject dog in owner.dogs)
                {
                    dog.GetComponent<EnemyDog>().ChangeState<DogPatrolState>();
                }
            }
        }
        else { owner.agent.SetDestination(owner.agent.transform.position); }
    }
}
#region dogLegacy
//owner.doggo.ChangeState<DogFetchState>();
//Debug.Log(bustedDistance);
// owner.doggo.agent.SetDestination(owner.transform.position);
//owner.agent.SetDestination(owner.player.transform.position);
//  owner.doggo.SwitchToFollow(owner.agent.transform.position);
//owner.doggo.agent.SetDestination(owner.player.transform.position);
//ChaseEvent chaseEvent = new ChaseEvent();
//chaseEvent.gameObject = owner.gameObject;
//chaseEvent.eventDescription = "Chasing Enemy";
//EventSystem.Current.FireEvent(chaseEvent);

//SoundEvent soundEvent = new SoundEvent();
//soundEvent.gameObject = owner.gameObject;
//soundEvent.eventDescription = "Chasing Sound";
//soundEvent.audioClip = audioSpeaker;

//EventSystem.Current.FireEvent(soundEvent);
#endregion