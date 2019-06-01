//Main Author: Emil Dahl
//Secondary Author: Paschalis Tolios

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/DogFetchState")]
public class DogFetchState : DogBaseState
{
    private float chaseDistance;
    private float hearingRange;
    private const float bustedDistance = 2f;
    private UnitDeathEventInfo deathInfo;

    public override void EnterState()
    {
        base.EnterState();
        EventSystem.Current.RegisterListener<UnitDeathEventInfo>(HandleDeath);
    }
    public override void ToDo()
    {
        if (!GameController.isPaused)
        {
            owner.agent.SetDestination(owner.player.transform.position);
            if (owner.inSafeZone)
                owner.ChangeState<DogPatrolState>();
            if (Vector3.Distance(owner.transform.position, owner.player.transform.position) < bustedDistance)
            {
                KillPlayer();
            }
        }
        else { owner.agent.SetDestination(owner.agent.transform.position); }
    }

    void HandleDeath(UnitDeathEventInfo death)
    {
        owner.ChangeState<DogPatrolState>();
    }
}
        //Debug.Log(Vector3.Distance(owner.transform.position, owner.player.transform.position));
