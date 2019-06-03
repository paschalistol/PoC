//Main Author: Emil Dahl
//Secondary Author: Paschalis Tolios
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/PatrolState")]
public class PatrolState : EnemyBaseState
{
    // Attributes

    private GameObject[] points;
    private const float noiceDetection = 5f;
    private float distanceToPlayer, distanceToPoint, lightRange, maxSpeed, chaseDistance;
    [SerializeField] private float lightIntensity = 15;
    private bool usedOnce = false;
    [SerializeField] private AudioClip clip;
    private int currentPoint = 0;
    private SoundEvent sound;

    // Methods
    public override void EnterState()
    {
        base.EnterState();

        points = owner.GetComponent<PatrolPoints>().GetPoints();
        ChooseClosest();

        owner.flashLight.GetComponent<Light>().intensity = lightIntensity;
        owner.flashLight.GetComponent<Light>().color = Color.white;
        lightRange = owner.flashLight.GetComponent<Light>().range;
            if (!owner.agent.hasPath)
                owner.agent.isStopped = false;

    }

    public override void ToDo()
    {
        //Debug.Log("isPaused - in Patrol: " + GameController.isPaused);
        if (!GameController.isPaused)
        {


            fieldOfView = Vector3.Angle(owner.transform.position, owner.player.transform.position);

            owner.agent.SetDestination(points[currentPoint].transform.position);

            distanceToPoint = Vector3.Distance(owner.transform.position, points[currentPoint].transform.position);
            distanceToPlayer = Vector3.Distance(owner.transform.position, owner.player.transform.position);


            if (distanceToPoint < 1)
            {
                currentPoint = (currentPoint + 1) % points.Length;
            }

            if (!usedOnce && clip != null && sound == null)
            {
                sound = new SoundEvent();
                sound.audioClip = clip;
                sound.looped = true;
                sound.parent = owner.gameObject;

                EventSystem.Current.FireEvent(sound);
                usedOnce = true;

            }

            Debug.Log(owner.name + " f ");

            if (LineOfSight() || GameController.activatedAlarm)
                owner.ChangeState<ChaseState>();
            else if (distanceToPlayer < hearingRange && owner.player.GetComponent<CharacterStateMachine>().GetMaxSpeed() > soundFromFeet
                && Input.anyKeyDown)
                owner.ChangeState<InvestigationState>();



            if ((LineOfSight() && distanceToPlayer < hearingRange && owner.player.GetComponent<CharacterStateMachine>().GetMaxSpeed() > soundFromFeet
                && Input.anyKeyDown) || GameController.activatedAlarm)
            {


            }
        }
        else { owner.agent.SetDestination(owner.agent.transform.position); }
    }

    private void ChooseClosest()
    {
        int closest = 0;
        for (int i = 0; i < points.Length; i++)
        {
            float dist = Vector3.Distance(owner.transform.position, points[i].transform.position);
            if (dist < Vector3.Distance(owner.transform.position, points[closest].transform.position))
                closest = i;
        }
        currentPoint = closest;
    }
    #region PatrolLegacy
    //chaseDistance = owner.GetFieldOfView();
    // private CharacterStateMachine charStateM;
    //lightAngle = lightField.spotAngle;
    #endregion

}
