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
    private Vector3 startPosition;
    [SerializeField] private float lightIntensity = 15;



    public override void EnterState()
    {
        base.EnterState();

        investigatePosition = owner.player.transform.position;
        startPosition = owner.agent.transform.position;
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
            Debug.Log("Does the owner have a path? : " + owner.agent.hasPath);
            
            owner.agent.SetDestination(investigatePosition);
            if(Vector3.Distance(owner.agent.transform.position, investigatePosition) < 2f)
            {
                owner.agent.isStopped = true;
                currentTime = 0f;
            }

          

            
            currentTime -= Time.deltaTime;
            
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
