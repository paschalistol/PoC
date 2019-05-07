
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/PatrolState")]
public class PatrolState : EnemyBaseState
{
    // Attributes
    
    private GameObject[] points;
    //private float chaseDistance;
    private float hearingRange;
    private float maxSpeed;
    private const float noiceDetection = 5f;
   // private CharacterStateMachine charStateM;
    private float lightRange;
    

    private int currentPoint = 0;

    // Methods
    public override void EnterState()
    {
        base.EnterState();
        //chaseDistance = owner.GetFieldOfView();
        
        hearingRange = owner.GetHearingDistance();
       
        points = owner.GetComponent<PatrolPoints>().GetPoints();
        ChooseClosest();

        owner.flashLight.GetComponent<Light>().intensity = 15;
        owner.flashLight.GetComponent<Light>().color = Color.white;
        lightRange = owner.flashLight.GetComponent<Light>().range;
    }

    public override void ToDo()
    {
       // fieldOfView = Vector3.Angle(owner.transform.position, owner.player.transform.position);
       // lightAngle = lightField.spotAngle;

        owner.agent.SetDestination(points[currentPoint].transform.position);
       
        if (Vector3.Distance(owner.transform.position, points[currentPoint].transform.position) < 1)
        {
            currentPoint = (currentPoint + 1) % points.Length;
           
        }
        if ((LineOfSight() && Vector3.Distance(owner.transform.position, owner.player.transform.position) < lightRange) ||
            (Vector3.Distance(owner.transform.position, owner.player.transform.position) < hearingRange && 
            owner.player.GetComponent<CharacterStateMachine>().GetMaxSpeed() > noiceDetection))
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

    
}
