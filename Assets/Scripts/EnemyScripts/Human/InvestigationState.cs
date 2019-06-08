//Main Author: Emil Dahl

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class handles the investigate position behavior
/// </summary>
[CreateAssetMenu(menuName = "Enemy/InvestigationState")]
public class InvestigationState : EnemyBaseState
{
    [SerializeField] private float lightIntensity = 15;
    private Vector3 investigatePosition;
    private float chaseDistance, distanceToPlayer, lightRange, currentTime;
    private const float startTime = 10f;
    private const float stopDistance = 2f;

    public override void EnterState()
    {
        base.EnterState();

        investigatePosition = owner.player.transform.position;
        currentTime = startTime;

        lightRange = owner.flashLight.GetComponent<Light>().range;
        owner.flashLight.GetComponent<Light>().intensity = lightIntensity;
        owner.flashLight.GetComponent<Light>().color = Color.white;
        if (owner.agent.hasPath)
            owner.agent.isStopped = false;
    }
    public override void ToDo()
    {
        if (!GameController.isPaused)
        {            
            owner.agent.SetDestination(investigatePosition);
            currentTime -= Time.deltaTime;

            if (Vector3.Distance(owner.agent.transform.position, investigatePosition) < stopDistance)
            {
                owner.agent.isStopped = true;
                currentTime = 0f;
            }
            
            if ((!owner.agent.hasPath && InRangeCheck(distanceToPlayer)) && owner.agent.isStopped == false || LineOfSight() || GameController.activatedAlarm)
                owner.ChangeState<ChaseState>();
            else
                owner.agent.isStopped = false;

            if (currentTime <= 0)
            {
                owner.agent.isStopped = false; 
                owner.ChangeState<PatrolState>();
            }

        }
        else { owner.agent.SetDestination(owner.agent.transform.position); }
    }
}
