using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/ChaseState")]
public class ChaseState : EnemyBaseState
{

    private float chaseDistance;
    private float hearingRange;
    [SerializeField] private float bustedDistance;

    public override void EnterState()
    {
        base.EnterState();
        hearingRange = owner.GetHearingDistance();
        chaseDistance = owner.GetFieldOfView();
        owner.flashLight.GetComponent<Light>().intensity = 25;
        owner.flashLight.GetComponent<Light>().color = Color.red;
        

    }
    public override void ToDo()
    {
        if ((LineOfSight() && Vector3.Distance(owner.transform.position, owner.player.transform.position) < chaseDistance) ||
            (Vector3.Distance(owner.transform.position, owner.player.transform.position) < hearingRange &&
            owner.player.GetComponent<CharacterStateMachine>().GetMaxSpeed() >= 5))
        {
            owner.agent.SetDestination(owner.player.transform.position);

            if (Vector3.Distance(owner.transform.position, owner.player.transform.position) < bustedDistance)
                owner.ChangeState<DetectionState>();
        }  else
            owner.ChangeState<PatrolState>();

    }
}
