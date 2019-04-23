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
    private float lightRange;

    public override void EnterState()
    {
        base.EnterState();
        owner.flashLight.GetComponent<Light>().intensity = 50;
        //chaseDistance = owner.GetFieldOfView();
        lightRange = owner.flashLight.GetComponent<Light>().range;
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

        fieldOfView = Vector3.Angle(owner.transform.position, owner.player.transform.position);
        lightAngle = lightField.spotAngle;

        if (LineOfSight() && Vector3.Distance(owner.transform.position, owner.player.transform.position) <= bustedDistance &&
           Vector3.Distance(owner.transform.position, owner.player.transform.position) < lightRange &&
                Vector3.Distance(owner.transform.position, owner.player.transform.position) < hearingDistance) 
        {
            //do something 

            SceneManager.LoadScene("EmilsTestScene");

        }
        else
        {
            owner.ChangeState<PatrolState>();
        }
       
        
    }
}
