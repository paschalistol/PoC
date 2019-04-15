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

        //  chaseDistance = owner.GetFieldOfView();
        //   hearingDistance = owner.GetHearingDistance();
        smellDistance = owner.GetSmellDistance();
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
        if (Vector3.Distance(owner.transform.position, owner.player.transform.position) < bustedDistance &&
            Vector3.Distance(owner.transform.position, owner.player.transform.position) < smellDistance)
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
