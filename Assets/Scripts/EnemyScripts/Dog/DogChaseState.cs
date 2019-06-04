//Main Author: Emil Dahl
//Secondary Author: Paschalis Tolios

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/DogChaseState")]
public class DogChaseState : DogBaseState
{

    private float smellDistance;
    private const float bustedDistance = 2f;
    private UnitDeathEventInfo deathInfo;
    private bool usedOnce;
    [SerializeField] private AudioClip barkSound;
    private StopSoundEvent stopSound;
    private SoundEvent sound;
    private MusicBasedOnChased musicBasedOnChased;

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
        sound.looped = false;
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
                //owner.agent.autoBraking = true;
                //owner.agent.transform.
                //owner.agent.transform.LookAt(owner.player.transform);
                owner.agent.transform.TransformDirection(owner.player.transform.position - owner.transform.position);

                if (Vector3.Distance(owner.transform.position, owner.player.transform.position) < bustedDistance)
                {
                    KillPlayer();
                    owner.ChangeState<DogPatrolState>();
                }
            }
        }
        else { owner.agent.SetDestination(owner.agent.transform.position); }
    }

    void HandleDeath(UnitDeathEventInfo death)
    {
        owner.ChangeState<DogPatrolState>();
    }

    protected void StartDogSound()
    {
    }

    protected void StopDogSound()
    {
        //stopSound = new StopSoundEvent();
        //stopSound.AudioPlayer = sound.objectInstatiated;
        //EventSystem.Current.FireEvent(stopSound);
    }
    public override void ExitState()
    {
        musicBasedOnChased = new MusicBasedOnChased();
        musicBasedOnChased.enemyChasing = false;
        EventSystem.Current.FireEvent(musicBasedOnChased);
    }

}
#region ChaseLegacy
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
