﻿
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/OldboiPatrolState")]
public class OldboiPatrolState : OldboiBaseState
{
    // Attributes
    
    private GameObject[] points;
    private float chaseDistance;
    private float hearingRange;
    private float maxSpeed;
    private CharacterStateMachine charStateM;
    

    private int currentPoint = 0;

    // Methods
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
       
        if (Vector3.Distance(owner.transform.position, points[currentPoint].transform.position) < 1)
        {
            currentPoint = (currentPoint + 1) % points.Length;
           
        }




        if ((LineOfSight() && Vector3.Distance(owner.transform.position, owner.player.transform.position) < chaseDistance) ||
            (Vector3.Distance(owner.transform.position, owner.player.transform.position) < hearingRange && 
            owner.player.GetComponent<CharacterStateMachine>().GetMaxSpeed() > 5))
        {
           
            owner.ChangeState<OldboiChaseState>();
        }
        //||

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

    
}