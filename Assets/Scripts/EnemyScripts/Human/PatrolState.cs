//Main Author: Emil Dahl
//Secondary Author: Paschalis Tolios
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class handles enemy patrol behavior 
/// </summary>
[CreateAssetMenu(menuName = "Enemy/PatrolState")]
public class PatrolState : EnemyBaseState
{

    #region SerializedVariables
    [SerializeField] private float lightIntensity = 15;
    [SerializeField] private AudioClip clip;
    [SerializeField] private float volume = 0f;
    #endregion
    private GameObject[] points;
    private SoundEvent sound;
    private float distanceToPlayer, distanceToPoint, lightRange, maxSpeed, chaseDistance, dist;
    private const float noiceDetection = 5f;
    private int currentPoint = 0;
    private int closest;
    private bool usedOnce = false;

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
        if (!GameController.isPaused)
        {
            owner.agent.SetDestination(points[currentPoint].transform.position);

            distanceToPoint = Vector3.Distance(owner.transform.position, points[currentPoint].transform.position);
            distanceToPlayer = Vector3.Distance(owner.transform.position, owner.player.transform.position);

            if (distanceToPoint < 1)
            {
                currentPoint = (currentPoint + 1) % points.Length;
            }


            if (LineOfSight() || GameController.activatedAlarm)
                owner.ChangeState<ChaseState>();
            else if (distanceToPlayer < hearingRange && owner.player.GetComponent<CharacterStateMachine>().GetMaxSpeed() > soundFromFeet
                && Input.anyKeyDown)
                owner.ChangeState<InvestigationState>();

            if (!usedOnce && clip != null && sound == null)
            {
                sound = new SoundEvent();
                sound.audioClip = clip;
                sound.looped = true;
                sound.parent = owner.gameObject;
                sound.volume = volume;

                EventSystem.Current.FireEvent(sound);
                usedOnce = true;
            }

        }
        else { owner.agent.SetDestination(owner.agent.transform.position); }
    }

    private void ChooseClosest()
    {
        closest = 0;
        for (int i = 0; i < points.Length; i++)
        {
            dist = Vector3.Distance(owner.transform.position, points[i].transform.position);
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
