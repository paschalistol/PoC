//Main Author: Emil Dahl
//Secondary Author: Paschalis Tolios
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/PatrolState")]
public class PatrolState : EnemyBaseState
{
    // Attributes
    
    private GameObject[] points;
    private const float noiceDetection = 5f;
    private float distanceToPlayer, distanceToPoint, lightRange, maxSpeed, hearingRange, chaseDistance;
    

    private int currentPoint = 0;

    // Methods
    public override void EnterState()
    {
        base.EnterState();
        
        hearingRange = owner.GetHearingDistance();
       
        points = owner.GetComponent<PatrolPoints>().GetPoints();
        ChooseClosest();

        owner.flashLight.GetComponent<Light>().intensity = 15;
        owner.flashLight.GetComponent<Light>().color = Color.white;
        lightRange = owner.flashLight.GetComponent<Light>().range;
    }

    public override void ToDo()
    {
       fieldOfView = Vector3.Angle(owner.transform.position, owner.player.transform.position);

        owner.agent.SetDestination(points[currentPoint].transform.position);

        distanceToPoint = Vector3.Distance(owner.transform.position, points[currentPoint].transform.position);
        distanceToPlayer = Vector3.Distance(owner.transform.position, owner.player.transform.position);


        if (distanceToPoint < 1)
        {
            currentPoint = (currentPoint + 1) % points.Length;
        }

        if (LineOfSight() || (distanceToPlayer < hearingRange && owner.player.GetComponent<CharacterStateMachine>().GetMaxSpeed() > 5) && Input.anyKeyDown)
        {     
            owner.ChangeState<ChaseState>();
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
    #region PatrolLegacy
    //chaseDistance = owner.GetFieldOfView();
    // private CharacterStateMachine charStateM;
    //lightAngle = lightField.spotAngle;
    #endregion

}
