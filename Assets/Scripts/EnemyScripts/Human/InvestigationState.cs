//Main Author: Emil Dahl
//Secondary Author: Paschalis Tolios

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/InvestigationState")]
public class InvestigationState : EnemyBaseState
{
    private MusicBasedOnChased musicBasedOnChased;
    private UnitDeathEventInfo deathInfo;
    private GameObject audioSpeaker;
    private float chaseDistance, distanceToPlayer, lightRange;
    private const float bustedDistance = 2f;
    private Vector3 investigatePosition;
    private const float startTime = 10f;
    private float currentTime;
    private bool usedOnce;

    public override void EnterState()
    {
        base.EnterState();

        investigatePosition = owner.player.transform.position;
        currentTime = startTime;

        lightRange = owner.flashLight.GetComponent<Light>().range;
        owner.flashLight.GetComponent<Light>().intensity = 15;
        owner.flashLight.GetComponent<Light>().color = Color.magenta;
        musicBasedOnChased = new MusicBasedOnChased();
        musicBasedOnChased.enemyChasing = true;
        EventSystem.Current.FireEvent(musicBasedOnChased);

    }
    public override void ToDo()
    {
        owner.agent.SetDestination(investigatePosition);
        Debug.Log(currentTime);

        currentTime -= Time.deltaTime;

        if (currentTime <= 0)
        {
            owner.ChangeState<PatrolState>();
        }
        else if (LineOfSight() || (distanceToPlayer < hearingRange && owner.player.GetComponent<CharacterStateMachine>().GetMaxSpeed() > 5)
                 && Input.anyKeyDown)
        {
            owner.ChangeState<ChaseState>();
        }
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
//hearingRange = owner.GetHearingDistance();
//if ((owner.agent.remainingDistance == 2f))
//{
//    Debug.Log(currentTime);
//}
#endregion