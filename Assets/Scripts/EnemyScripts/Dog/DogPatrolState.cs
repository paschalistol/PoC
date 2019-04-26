﻿
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/DogPatrolState")]
public class DogPatrolState : DogBaseState
{
    // Attributes
    
    private GameObject[] DogPoints;
    private GameObject currentPoint;
    
    private float smellDistance;
    
    private int Point;

    // Methods
    public override void EnterState()
    {
        base.EnterState();
        
        smellDistance = owner.GetSmellDistance();
       
       
        DogPoints = owner.GetComponent<DogPatrolPoints>().GetPoints();
        ChooseRandom();
       
        

    }

    public override void ToDo()
    {
        owner.agent.SetDestination(DogPoints[Point].transform.position);
       // Debug.Log(owner.agent.destination + " " + DogPoints[Point].transform.position);
        if (Vector3.Distance(owner.transform.position, DogPoints[Point].transform.position) < 5)
        {
            
            ChooseRandom();
        }
        if (Vector3.Distance(owner.transform.position, owner.player.transform.position) < smellDistance)
        {
           owner.ChangeState<DogChaseState>();
        }
    }

   /* private void ChooseClosest()
    {
        int closest = 0;
        for (int i = 0; i < Dogpoints.Length; i++)
        {
            float dist = Vector3.Distance(owner.transform.position, Dogpoints[i].transform.position);
            if (dist < Vector3.Distance(owner.transform.position, Dogpoints[closest].transform.position))
                closest = i;
        }
        currentPoint = closest;
    }*/

    protected void ChooseRandom()
    {
        Point = Random.Range(0, DogPoints.Length);
       // owner.agent.SetDestination(DogPoints[Point].transform.position);
       
    }


}