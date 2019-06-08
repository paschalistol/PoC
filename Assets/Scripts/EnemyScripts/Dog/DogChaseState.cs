//Main Author: Emil Dahl
//Secondary Author: Paschalis Tolios

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class handles the basic "Chase enemy" behavior for the dog
/// </summary>
[CreateAssetMenu(menuName = "Enemy/DogChaseState")]
public class DogChaseState : DogBaseState
{

    [SerializeField] private AudioClip barkSound;
    [SerializeField] private bool loopedSound = false;
    private UnitDeathEventInfo deathInfo;
    private StopSoundEvent stopSound;
    private SoundEvent sound;
    private MusicBasedOnChased musicBasedOnChased;
    private float smellDistance;
    private const float bustedDistance = 2f;
    private bool usedOnce;

    public override void EnterState()
    {
        base.EnterState();
        smellDistance = owner.GetSmellDistance();
        EventSystem.Current.RegisterListener<UnitDeathEventInfo>(HandleDeath);
        owner.isInChase = true;
        musicBasedOnChased = new MusicBasedOnChased();
        musicBasedOnChased.enemyChasing = true;
        EventSystem.Current.FireEvent(musicBasedOnChased);
        sound = new SoundEvent();
        sound.eventDescription = "The dog barks";
        sound.audioClip = barkSound;
        sound.looped = loopedSound;
        sound.parent = owner.gameObject;
        EventSystem.Current.FireEvent(sound);

    }
    /// <summary>
    /// Decides if the dog will return to patrol or attack while chasing the player.
    /// </summary>
    public override void ToDo()
    {
        if (!GameController.isPaused)
        {

            if (Vector3.Distance(owner.transform.position, owner.player.transform.position) >= smellDistance
                || owner.inSafeZone)
            {
                owner.ChangeState<DogPatrolState>();
            }
            else
            {
                owner.agent.SetDestination(owner.player.transform.position);

                if (Vector3.Distance(owner.transform.position, owner.player.transform.position) < bustedDistance)
                {
                    KillPlayer();
                    owner.ChangeState<DogPatrolState>();
                }
            }
        }
        else { owner.agent.SetDestination(owner.agent.transform.position); }
    }

    /// <summary>
    /// If the player dies the dog returns to patrolling
    /// </summary>
    /// <param name="death"></param>
    void HandleDeath(UnitDeathEventInfo death)
    {
        owner.ChangeState<DogPatrolState>();
    }

    /// <summary>
    /// Stops the sound that has been created for the dog
    /// </summary>
    protected void StopDogSound()
    {
        stopSound = new StopSoundEvent();
        stopSound.AudioPlayer = sound.objectInstatiated;
        EventSystem.Current.FireEvent(stopSound);
    }
    public override void ExitState()
    {
        musicBasedOnChased = new MusicBasedOnChased();
        musicBasedOnChased.enemyChasing = false;
        EventSystem.Current.FireEvent(musicBasedOnChased);
    }

}
#region DogChaseLegacy
//public override void ExitState()
//{
//    Debug.Log("Walla");
//    owner.agent.transform.position = owner.agent.transform.position;
//    base.ExitState();
//}
//   audioSpeaker = owner.audioSpeaker;
//ChaseEvent chaseEvent = new ChaseEvent();
//chaseEvent.gameObject = owner.gameObject;
//chaseEvent.eventDescription = "Chasing Enemy";
//chaseEvent.audioSpeaker = audioSpeaker;
//private GameObject audioSpeaker;
//EventSystem.Current.FireEvent(chaseEvent);
// hearingRange = owner.GetHearingDistance();
// chaseDistance = owner.GetFieldOfView();
//  private float chaseDistance;
//  private float hearingRange;
#endregion
