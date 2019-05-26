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
                deathInfo = new UnitDeathEventInfo();
                deathInfo.eventDescription = "U big dead lmao!";
                deathInfo.spawnPoint = owner.player.GetComponent<CharacterStateMachine>().currentCheckPoint;
                deathInfo.deadUnit = owner.player.transform.gameObject;
                EventSystem.Current.FireEvent(deathInfo);

                owner.ChangeState<DogPatrolState>();
            }
        }
        else { owner.agent.SetDestination(owner.agent.transform.position); }

    }
}
        //Debug.Log(Vector3.Distance(owner.transform.position, owner.player.transform.position));
