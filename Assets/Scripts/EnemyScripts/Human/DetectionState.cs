using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(menuName = "Enemy/DetectionState")]
public class DetectionState : EnemyBaseState
{
    [SerializeField]private float bustedDistance;
    private float chaseDistance;
    private float hearingDistance;

    public override void EnterState()
    {
        base.EnterState();
        owner.flashLight.GetComponent<Light>().intensity = 50;
        chaseDistance = owner.GetFieldOfView();
        hearingDistance = owner.GetHearingDistance();
    }

    // Update is called once per frame

     /**
      * comes into detection = changes color
      * isn't able to run if statement 
      * stays this way forever cuz no other conditions
      * 
      * 
      * **/
    public override void ToDo()
    {
        if (LineOfSight() && Vector3.Distance(owner.transform.position, owner.player.transform.position) <= bustedDistance &&
            Vector3.Distance(owner.transform.position, owner.player.transform.position) < chaseDistance &&
                Vector3.Distance(owner.transform.position, owner.player.transform.position) < hearingDistance) 
        {
            //do something 

            SceneManager.LoadScene("test");

        }
        else
        {
            owner.ChangeState<PatrolState>();
        }
       
        
    }
}
