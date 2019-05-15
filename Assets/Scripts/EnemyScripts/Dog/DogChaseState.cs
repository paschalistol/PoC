//Main Author: Emil Dahl
//Secondary Author: Paschalis Tolios

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
    private GameObject audioSpeaker;
public bool inSafezone = false;
    public override void EnterState()
    {
        base.EnterState();
        // hearingRange = owner.GetHearingDistance();
        // chaseDistance = owner.GetFieldOfView();
        smellDistance = owner.GetSmellDistance();
        //   audioSpeaker = owner.audioSpeaker;
        //ChaseEvent chaseEvent = new ChaseEvent();
        //chaseEvent.gameObject = owner.gameObject;
        //chaseEvent.eventDescription = "Chasing Enemy";
        //chaseEvent.audioSpeaker = audioSpeaker;

        
    //EventSystem.Current.FireEvent(chaseEvent);


}
    public override void ToDo()
    {

         void OnTriggerEnter(Collision collision)
        {
            if (collision != null)
            {
                Debug.Log("yeet");
            }
            if (collision.transform.tag == "Safezone")
            {
                inSafezone = true;
                Debug.Log("in");
            }
        }

         void OnTriggerExit(Collision collision)
        {
            if (collision.transform.tag == "Safezone")
            {
                inSafezone = false;
                Debug.Log("out");
            }
        }

        if (Vector3.Distance(owner.transform.position, owner.player.transform.position) >= smellDistance || inSafezone)
        {
            owner.ChangeState<DogPatrolState>();
            
        }
        else
        {
            owner.agent.SetDestination(owner.player.transform.position);
            if (Vector3.Distance(owner.transform.position, owner.player.transform.position) < bustedDistance)
                owner.ChangeState<DogDetectionState>();
        }


    }
}
