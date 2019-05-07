//Main Author: Emil Dahl
//Secondary Author: Paschalis Tolios

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/OldboiFetchState")]
public class DogFetchState : DogBaseState
{

    private float chaseDistance;
    private float hearingRange;
    [SerializeField] private float bustedDistance;

    public override void EnterState()
    {
        base.EnterState();  
    }
    public override void ToDo()
    {
        owner.agent.SetDestination(owner.player.transform.position);

        if (Vector3.Distance(owner.agent.transform.position, owner.player.transform.position) < bustedDistance)
        {
            owner.ChangeState<DogDetectionState>();
        }
    }
}
