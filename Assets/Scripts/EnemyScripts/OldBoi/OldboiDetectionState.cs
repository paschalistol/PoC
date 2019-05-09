//Main Author: Emil Dahl
//Secondary Author: Paschalis Tolios

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(menuName = "Enemy/OldboiDetectionState")]
public class OldboiDetectionState : OldboiBaseState
{
    [SerializeField]private float bustedDistance;
    private float chaseDistance;
    private float hearingDistance;

    public override void EnterState()
    {
        base.EnterState();
        chaseDistance = owner.GetFieldOfView();
        hearingDistance = owner.GetHearingDistance();
    }

    // Update is called once per frame

    
    public override void ToDo()
    {
        if (LineOfSight() && Vector3.Distance(owner.transform.position, owner.player.transform.position) <= bustedDistance &&
            Vector3.Distance(owner.transform.position, owner.player.transform.position) < chaseDistance) 
        {

            Debug.Log("Deathcollider is not null!");
            UnitDeathEventInfo deathInfo = new UnitDeathEventInfo();
            deathInfo.eventDescription = "U big dead lmao!";
            deathInfo.spawnPoint = owner.player.transform.gameObject.GetComponent<CharacterStateMachine>().currentCheckPoint;

            deathInfo.deadUnit = owner.player.transform.gameObject;
            EventSystem.Current.FireEvent(deathInfo);

        }
        else
        {
            owner.ChangeState<OldboiPatrolState>();
        }
       
        
    }
}
