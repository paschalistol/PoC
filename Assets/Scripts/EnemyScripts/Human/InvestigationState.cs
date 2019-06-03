//Main Author: Emil Dahl

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
    [SerializeField] private float lightIntensity = 15;


    public override void EnterState()
    {
        base.EnterState();

        investigatePosition = owner.player.transform.position;
        currentTime = startTime;

        lightRange = owner.flashLight.GetComponent<Light>().range;
        owner.flashLight.GetComponent<Light>().intensity = lightIntensity;
        owner.flashLight.GetComponent<Light>().color = Color.white;
    }
    public override void ToDo()
    {
        if (!GameController.isPaused)
        {
            owner.agent.SetDestination(investigatePosition);
            currentTime -= Time.deltaTime;
            
            if ((!owner.agent.hasPath && InRangeCheck(distanceToPlayer)) || LineOfSight() || GameController.activatedAlarm)
                owner.ChangeState<ChaseState>();
            else
                owner.agent.isStopped = false;
            
            if (currentTime <= 0)
                owner.ChangeState<PatrolState>();


        }
        else { owner.agent.SetDestination(owner.agent.transform.position); }
    }
}
