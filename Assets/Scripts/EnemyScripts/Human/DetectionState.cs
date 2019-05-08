//Main Author: Emil Dahl
//Secondary Author: Paschalis Tolios

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(menuName = "Enemy/DetectionState")]
public class DetectionState : EnemyBaseState
{
    [SerializeField]private float bustedDistance;
    //private float chaseDistance;
    private float hearingDistance;
    private float lightRange;

    public override void EnterState()
    {
        base.EnterState();
        owner.flashLight.GetComponent<Light>().intensity = 50;
        //chaseDistance = owner.GetFieldOfView();
        lightRange = owner.flashLight.GetComponent<Light>().range;
        hearingDistance = owner.GetHearingDistance();
        
    }

    
    public override void ToDo()
    {

        //fieldOfView = Vector3.Angle(owner.transform.position, owner.player.transform.position);
        //lightAngle = lightField.spotAngle;

        if (LineOfSight() && Vector3.Distance(owner.transform.position, owner.player.transform.position) <= bustedDistance &&
           Vector3.Distance(owner.transform.position, owner.player.transform.position) < lightRange &&
                Vector3.Distance(owner.transform.position, owner.player.transform.position) < hearingDistance) 
        {
            //do something 

            Debug.Log("Deathcollider is not null!");
            UnitDeathEventInfo deathInfo = new UnitDeathEventInfo();
            deathInfo.eventDescription = "U big dead lmao!";

            deathInfo.deadUnit = owner.player.transform.gameObject;
            deathInfo.spawnPoint = owner.player.transform.gameObject.GetComponent<CharacterStateMachine>().currentCheckPoint;
            EventSystem.Current.FireEvent(deathInfo);

        }
        else
        {
            owner.ChangeState<PatrolState>();
        }
       
        
    }
}
