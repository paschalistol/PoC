//Main Author: Emil Dahl
//Secondary Author: Paschalis Tolios

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/DogFetchState")]
public class DogFetchState : DogBaseState
{
    private float chaseDistance, hearingRange;
    private const float bustedDistance = 2f;
    private UnitDeathEventInfo deathInfo;
    private float currentTime;
    [SerializeField] private float animationTime = 0.05f;
    private bool activatedAnimation = true;
    private Canvas canvas;
    [SerializeField] private AudioClip growlSound;
    private SoundEvent sound;
    private StopSoundEvent stopSound;
    private bool usedOnce;
    [SerializeField] private bool loopedSound = true;


    public override void EnterState()
    {
        base.EnterState();
        EventSystem.Current.RegisterListener<UnitDeathEventInfo>(HandleDeath);
        //canvas.GetComponentInChildren<Canvas>();
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

            //ChangeUI();
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

    protected void ChangeUI()
    {
        currentTime -= Time.deltaTime;
        if (currentTime <= 0)
        {
            activatedAnimation = !activatedAnimation;
            currentTime = animationTime;
            canvas.gameObject.SetActive(activatedAnimation);
        }
    }

    protected void StartDogSound()
    {
        sound = new SoundEvent();
        sound.audioClip = growlSound;
        sound.looped = loopedSound;
        sound.parent = owner.gameObject;
        EventSystem.Current.FireEvent(sound);
    }

    protected void StopDogSound()
    {
        stopSound = new StopSoundEvent();
        stopSound.AudioPlayer = sound.objectInstatiated;
        EventSystem.Current.FireEvent(stopSound);
    }

    //public override void ExitState()
    //{
    //    base.ExitState();
    //    //StopDogSound();
    //}
}
        //Debug.Log(Vector3.Distance(owner.transform.position, owner.player.transform.position));
