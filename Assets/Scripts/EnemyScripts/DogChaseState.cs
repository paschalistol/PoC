using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/DogChaseState")]
public class DogChaseState : DogBaseState
{

  //  private float chaseDistance;
  //  private float hearingRange;
    private float smellDistance;
    [SerializeField] private float bustedDistance;

    public override void EnterState()
    {
        base.EnterState();
        // hearingRange = owner.GetHearingDistance();
        // chaseDistance = owner.GetFieldOfView();
        smellDistance = owner.GetSmellDistance();
        

    }
    public override void ToDo()
    {
        if (Vector3.Distance(owner.transform.position, owner.player.transform.position) < smellDistance)
        {
            owner.agent.SetDestination(owner.player.transform.position);

            if (Vector3.Distance(owner.transform.position, owner.player.transform.position) < bustedDistance)
                owner.ChangeState<DetectionState>();
        }  else
            owner.ChangeState<PatrolState>();

    }
}
