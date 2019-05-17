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
    [SerializeField] private float bustedDistance;
    private UnitDeathEventInfo deathInfo;

    public override void EnterState()
    {
        base.EnterState();  
    }
    public override void ToDo()
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
        //Debug.Log(Vector3.Distance(owner.transform.position, owner.player.transform.position));

    }
}
