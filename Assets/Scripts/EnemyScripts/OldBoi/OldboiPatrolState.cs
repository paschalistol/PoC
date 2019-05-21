//Main Author: Emil Dahl
//Secondary Author: Paschalis Tolios
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/OldboiPatrolState")]
public class OldboiPatrolState : OldboiBaseState
{  
    private GameObject[] points;
    private CharacterStateMachine charStateM;
    private int currentPoint = 0;
    private float chaseDistance, hearingRange, maxSpeed, distanceToPlayer;

    public override void EnterState()
    {
        base.EnterState();
        chaseDistance = owner.GetFieldOfView();
        hearingRange = owner.GetHearingDistance();
        points = owner.GetComponent<OldboiPatrolPoints>().GetPoints();
        ChooseClosest();
    }

    public override void ToDo()
    {
       
        owner.agent.SetDestination(points[currentPoint].transform.position);
        distanceToPlayer = Vector3.Distance(owner.transform.position, owner.player.transform.position);


        if (Vector3.Distance(owner.transform.position, points[currentPoint].transform.position) < 1)
        {
            currentPoint = (currentPoint + 1) % points.Length;
           
        }

        if ((LineOfSight() && distanceToPlayer < chaseDistance) || (Vector3.Distance(owner.transform.position, owner.player.transform.position) < hearingRange && 
            (owner.player.GetComponent<CharacterStateMachine>().GetMaxSpeed() > 5 && Input.anyKeyDown)))
        {

            owner.ChangeState<OldboiAlertState>();
        }
    }

    private void ChooseClosest()
    {
        int closest = 0;
        for (int i = 0; i < points.Length; i++)
        {
            float dist = Vector3.Distance(owner.transform.position, points[i].transform.position);
            if (dist < Vector3.Distance(owner.transform.position, points[closest].transform.position))
                closest = i;
        }
        currentPoint = closest;
    }

    public override void ExitState()
    {
        base.ExitState();
        owner.agent.SetDestination(owner.transform.position);
    }


}
