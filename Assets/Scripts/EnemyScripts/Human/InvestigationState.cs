//Main Author: Emil Dahl
//Secondary Author: Paschalis Tolios

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/InvestigationState")]
public class InvestigationState : EnemyBaseState
{
    private float chaseDistance, distanceToPlayer, lightRange, currentTime;
    private const float startTime = 10f;
    private const float hearingLevel = 5f;
    private Vector3 investigatePosition;

    public override void EnterState()
    {
        base.EnterState();

        investigatePosition = owner.player.transform.position;
        currentTime = startTime;

        lightRange = owner.flashLight.GetComponent<Light>().range;
        owner.flashLight.GetComponent<Light>().intensity = 15;
        owner.flashLight.GetComponent<Light>().color = Color.magenta;
    }
    public override void ToDo()
    {
        if (!GameController.isPaused)
        {

            owner.agent.SetDestination(investigatePosition);

            currentTime -= Time.deltaTime;

            if (currentTime <= 0)
            {
                owner.ChangeState<PatrolState>();
            }
            else if (LineOfSight() || (distanceToPlayer < hearingRange && owner.player.GetComponent<CharacterStateMachine>().GetMaxSpeed() > hearingLevel)
                     && Input.anyKeyDown)
            {
                owner.ChangeState<ChaseState>();
            }
        }
        else { owner.agent.SetDestination(owner.agent.transform.position); }
    }
}
