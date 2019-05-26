//Main Author: Emil Dahl
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
    private float chaseDistance, distanceToPlayer, lightRange;
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

            if ((LineOfSight() && distanceToPlayer < lightRange) || (distanceToPlayer < hearingRange &&
                owner.player.GetComponent<CharacterStateMachine>().GetMaxSpeed() > 5))
            {
                owner.agent.SetDestination(owner.player.transform.position);

                foreach (GameObject dog in owner.dogs)
                {
                    dog.GetComponent<EnemyDog>().ChangeState<DogFetchState>();
                }

                if (distanceToPlayer < bustedDistance)
                {
                    deathInfo = new UnitDeathEventInfo();
                    deathInfo.eventDescription = "U big dead lmao!";
                    deathInfo.spawnPoint = owner.player.GetComponent<CharacterStateMachine>().currentCheckPoint;
                    deathInfo.deadUnit = owner.player.transform.gameObject;
                    EventSystem.Current.FireEvent(deathInfo);
                }
            }
            else
            {
                if (distanceToPlayer > investigationDistance)
                    owner.ChangeState<InvestigationState>();

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