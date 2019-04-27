using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/OldboiChaseState")]
public class OldboiChaseState : OldboiBaseState
{

    private float chaseDistance;
    private float hearingRange;
    [SerializeField] private float bustedDistance;

    public override void EnterState()
    {
        base.EnterState();
        hearingRange = owner.GetHearingDistance();
        chaseDistance = owner.GetFieldOfView();
       
        

    }
    public override void ToDo()
    {
        
        if ((LineOfSight() && Vector3.Distance(owner.transform.position, owner.player.transform.position) < chaseDistance) ||
            (Vector3.Distance(owner.transform.position, owner.player.transform.position) < hearingRange &&
            owner.player.GetComponent<CharacterStateMachine>().GetMaxSpeed() >= 5))
        {
            
           owner.agent.SetDestination(owner.player.transform.position);
            //  owner.doggo.SwitchToFollow(owner.agent.transform.position);
            //owner.doggo.agent.SetDestination(owner.player.transform.position);
            owner.doggo.ChangeState<DogFetchState>();
            
           // owner.doggo.agent.SetDestination(owner.transform.position);
            Debug.Log("waddup");

            if (Vector3.Distance(owner.transform.position, owner.player.transform.position) < bustedDistance)
                owner.ChangeState<OldboiDetectionState>();
        }  else
            owner.ChangeState<OldboiPatrolState>();

    }
}
