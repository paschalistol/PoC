//Main Author: Emil Dahl
//Secondary Author: Paschalis Tolios

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class handles the fetch dog behavior
/// </summary>
[CreateAssetMenu(menuName = "Enemy/DogFetchState")]
public class DogFetchState : DogBaseState
{
    [SerializeField] private AudioClip growlSound;
    [SerializeField] private bool loopedSound = true;
    #region Events
    private SoundEvent sound;
    private StopSoundEvent stopSound;
    private UnitDeathEventInfo deathInfo;
    #endregion
    private const float bustedDistance = 2f;
    private bool activatedAnimation = true;
    private bool usedOnce;
    private float chaseDistance, hearingRange, currentTime;


    public override void EnterState()
    {
        base.EnterState();
        EventSystem.Current.RegisterListener<UnitDeathEventInfo>(HandleDeath);
    }
    public override void ToDo()
    {
        if (!GameController.isPaused)
        {
            if (!usedOnce)
            {
                StartDogSound();
                usedOnce = true;
            }

            owner.agent.SetDestination(owner.player.transform.position);
            if (owner.inSafeZone)
                owner.ChangeState<DogPatrolState>();
            if (Vector3.Distance(owner.transform.position, owner.player.transform.position) < bustedDistance)
            {
                KillPlayer();
            }
        }
        else { owner.agent.SetDestination(owner.agent.transform.position); }
    }

    void HandleDeath(UnitDeathEventInfo death)
    {
        StopDogSound();
        owner.ChangeState<DogPatrolState>();
    }


    /// <summary>
    /// Plays selected sound from the dogs position
    /// </summary>
    protected void StartDogSound()
    {
        sound = new SoundEvent();
        sound.audioClip = growlSound;
        sound.looped = loopedSound;
        sound.parent = owner.gameObject;
        EventSystem.Current.FireEvent(sound);
    }

    /// <summary>
    /// Stops the sound that has been created
    /// </summary>
    protected void StopDogSound()
    {
        stopSound = new StopSoundEvent();
        stopSound.AudioPlayer = sound.objectInstatiated;
        EventSystem.Current.FireEvent(stopSound);
    }

    #region legacy
    //public override void ExitState()
    //{
    //    base.ExitState();
    //    //StopDogSound();
    //}
//Debug.Log(Vector3.Distance(owner.transform.position, owner.player.transform.position));
    //protected void ChangeUI()
    //{
    //    currentTime -= Time.deltaTime;
    //    if (currentTime <= 0)
    //    {
    //        activatedAnimation = !activatedAnimation;
    //        currentTime = animationTime;
    //        canvas.gameObject.SetActive(activatedAnimation);
    //    }
    //}
    #endregion
}
