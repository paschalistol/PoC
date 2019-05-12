//Main Author: Emil Dahl
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/OldboiChaseState")]
public class OldboiChaseState : OldboiBaseState
{
 
    private float chaseDistance;
    private float hearingRange;
    [SerializeField] private float bustedDistance;
    private const float speed = 0.1f;

    public AudioClip audioSpeaker;

    public override void EnterState()
    {
        base.EnterState();
        hearingRange = owner.GetHearingDistance();
        chaseDistance = owner.GetFieldOfView();


       //ChaseEvent chaseEvent = new ChaseEvent();
       //chaseEvent.gameObject = owner.gameObject;
       //chaseEvent.eventDescription = "Chasing Enemy";
       //EventSystem.Current.FireEvent(chaseEvent);

        //SoundEvent soundEvent = new SoundEvent();
        //soundEvent.gameObject = owner.gameObject;
        //soundEvent.eventDescription = "Chasing Sound";
        //soundEvent.audioClip = audioSpeaker;

        //EventSystem.Current.FireEvent(soundEvent);

    }
    public override void ToDo()
    {
        

        Vector3 direction = owner.player.transform.position - owner.transform.position;
        Quaternion rotation = Quaternion.LookRotation(direction);
        owner.transform.rotation = Quaternion.Lerp(owner.transform.rotation, rotation, speed);

        Debug.Log(bustedDistance);

        if ((LineOfSight() && Vector3.Distance(owner.transform.position, owner.player.transform.position) < chaseDistance) ||
            (Vector3.Distance(owner.transform.position, owner.player.transform.position) < hearingRange &&
            owner.player.GetComponent<CharacterStateMachine>().GetMaxSpeed() >= 5))
        {

           
            //owner.agent.SetDestination(owner.player.transform.position);
            //  owner.doggo.SwitchToFollow(owner.agent.transform.position);
            //owner.doggo.agent.SetDestination(owner.player.transform.position);
            foreach(GameObject dog in owner.dogs){
                dog.GetComponent<EnemyDog>().ChangeState<DogFetchState>();
                
                //owner.doggo.ChangeState<DogFetchState>();
            }
          
            
           // owner.doggo.agent.SetDestination(owner.transform.position);
            Debug.Log("waddup");
         
            if (Vector3.Distance(owner.transform.position, owner.player.transform.position) < bustedDistance)
                owner.ChangeState<OldboiDetectionState>();

        }  else
            owner.ChangeState<OldboiPatrolState>();

    }
}
