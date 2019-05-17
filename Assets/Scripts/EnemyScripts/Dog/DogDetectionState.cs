//Main Author: Emil Dahl
//Secondary Author: Paschalis Tolios
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(menuName = "Enemy/DogDetectionState")]
public class DogDetectionState : DogBaseState
{
    [SerializeField]private float bustedDistance;
    //  private float chaseDistance;
    // private float hearingDistance;
    private float smellDistance;


    public override void EnterState()
    {
        base.EnterState();
            Debug.Log("dgfd");

        //  chaseDistance = owner.GetFieldOfView();
        //   hearingDistance = owner.GetHearingDistance();
        smellDistance = owner.GetSmellDistance();
    }

    public override void ToDo()
    {

        if (Vector3.Distance(owner.transform.position, owner.player.transform.position) < bustedDistance)
        {
            //do something 
            Debug.Log ("dgfd2");

            UnitDeathEventInfo deathInfo = new UnitDeathEventInfo();
            deathInfo.eventDescription = "U big dead lmao!";
            deathInfo.spawnPoint = owner.player.GetComponent<CharacterStateMachine>().currentCheckPoint;
            deathInfo.deadUnit = owner.player.transform.gameObject;
            EventSystem.Current.FireEvent(deathInfo);
            Debug.Log("GettingBustedByDoggo");

        }
        //else
        //{
        //    owner.ChangeState<DogPatrolState>();
        //}
       
        
    }
}
