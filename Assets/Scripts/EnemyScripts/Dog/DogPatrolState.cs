//Main Author: Emil Dahl
//Secondary Author: Paschalis Tolios

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class handles the behavior for dog patrolling
/// </summary>
[CreateAssetMenu(menuName = "Enemy/DogPatrolState")]
public class DogPatrolState : DogBaseState
{
    // Attributes
    
    private GameObject[] DogPoints;
    private GameObject currentPoint;
    private const float noiceDetection = 5f;
    private int Point;
    private float smellDistance, distanceToPoint, distanceToEnemy;

    public override void EnterState()
    {
        base.EnterState();
        smellDistance = owner.GetSmellDistance();
        DogPoints = owner.GetComponent<DogPatrolPoints>().GetPoints();
        ChooseRandom();
    }

    public override void ToDo()
    {
        if (!GameController.isPaused)
        {
            owner.agent.SetDestination(DogPoints[Point].transform.position);
            distanceToPoint = Vector3.Distance(owner.transform.position, DogPoints[Point].transform.position);
            if (distanceToPoint < noiceDetection)
            {
                ChooseRandom();
            }

            distanceToEnemy = Vector3.Distance(owner.transform.position, owner.player.transform.position);

            if (distanceToEnemy < smellDistance && !owner.inSafeZone)
            {
                owner.ChangeState<DogChaseState>();
            }
        }else { owner.agent.SetDestination(owner.agent.transform.position); }
    }

    /// <summary>
    /// Picks random position based on assigned patrol positions
    /// </summary>
    protected void ChooseRandom()
    {
        Point = Random.Range(0, DogPoints.Length);
    }


}
