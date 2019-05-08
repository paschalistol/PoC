//Main Author: Emil Dahl
//Secondary Author: Paschalis Tolios

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
    private const float noiceDetection = 5f;

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
        if (Vector3.Distance(owner.transform.position, DogPoints[Point].transform.position) < noiceDetection)
        {
            ChooseRandom();
        }
        if (Vector3.Distance(owner.transform.position, owner.player.transform.position) < smellDistance)
        {
           owner.ChangeState<DogChaseState>();
        }
    }

    protected void ChooseRandom()
    {
        Point = Random.Range(0, DogPoints.Length);
    }


}
