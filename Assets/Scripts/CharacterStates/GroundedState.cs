//Main Author: Paschalis Tolios
//Secondary author: Johan Ekman

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Character/GroundedState")]
public class GroundedState : CharacterBaseState
{

    public Animator anim;
    public float speed;
    public float direction;
    [SerializeField] private AudioClip groundedSound, footsteps;
    [SerializeField] private float dynamicFrictionCoeff = 0.35f;
    private float maxSpeedCoeff = 5;
    private const float movementMultiplier = 2f;
    private GameObject walkingParticles;
    private bool playingParticles;
    private SoundEvent walkingSound;
    private StopSoundEvent stopSoundEvent;

    public override void EnterState()
    {
        base.EnterState();
        walkingParticles = owner.walkingParticles; 
        dynamicFriction = dynamicFrictionCoeff;
        MaxSpeed = maxSpeedCoeff;
        walkingSound = new SoundEvent();
        SoundEvent soundEvent = new SoundEvent();
        soundEvent.gameObject = owner.gameObject;
        soundEvent.eventDescription = "Grounded Sound";
        soundEvent.audioClip = groundedSound;
        soundEvent.looped = false;
        if (soundEvent.audioClip != null)
        {
            EventSystem.Current.FireEvent(soundEvent);
        }
        //   anim = owner.GetComponent<Animator>();
    }

    public override void ToDo()
    {
        if (!GameController.isPaused)
        {
            Debug.Log(MaxSpeed);
            Gravity();
            #region Input
            Vector3 input = GetDirectionInput();

            CheckInput(input);
            #endregion
            ChangeCharRotation();
            if (input.magnitude > 0 && walkingSound.objectPlaying == null)
            {

                walkingSound.gameObject = owner.gameObject;
                walkingSound.eventDescription = "Grounded Sound";
                walkingSound.audioClip = footsteps;
                walkingSound.looped = true;
                if (walkingSound.audioClip != null)
                {
                    EventSystem.Current.FireEvent(walkingSound);
                }
            }
            if (input.magnitude == 0 && walkingSound.objectPlaying != null)
            {
                stopSoundEvent = new StopSoundEvent();
                stopSoundEvent.AudioPlayer = walkingSound.objectPlaying;
                stopSoundEvent.eventDescription = "Stop Sound";
                if (stopSoundEvent.AudioPlayer != null)
                {
                    EventSystem.Current.FireEvent(stopSoundEvent);
                }
            }
            //  Speed();


            #region Buttons
            if (Input.GetKeyDown(KeyCode.Space))
            {
                ApplyForce(new Vector3(0, jumpHeight, 0));
                //anim.SetTrigger("jump");

                //AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
                //if (Input.GetKeyDown(KeyCode.Space) && stateInfo.nameHash == runStateHash)
                //{
                //    anim.SetTrigger(jumpHash);
                //}
            }
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                MaxSpeed = maxSpeedCoeff * movementMultiplier;
            }
            else if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                MaxSpeed = maxSpeedCoeff;
            }
            #endregion

            DeathCollisionCheck();
            ReachingCheckPoint();
            //Trampoline();
            Bouncing();
            CollisionCheck();
            //ReachingGoal();
            owner.transform.position += Velocity * Time.deltaTime;


            if (TakingLift2() != null)
            {
                owner.ChangeState<OnLiftState>();
            }
            if (!IsGrounded())
            {
                owner.ChangeState<InTheAirState>();

            }
        }
    }
    public override void ExitState()
    {
        if (walkingSound.objectPlaying != null)
        {
            stopSoundEvent = new StopSoundEvent();
            stopSoundEvent.AudioPlayer = walkingSound.objectPlaying;
            stopSoundEvent.eventDescription = "Stop Sound";
            if (stopSoundEvent.AudioPlayer != null)
            {
                EventSystem.Current.FireEvent(stopSoundEvent);
            }
        }
        base.ExitState();
    }
    private ParticleEvent particleEvent;
    //checks for the current input. Creates walking particles. 
    private void CheckInput(Vector3 input)
    {
        if (input.magnitude <= 0)
        {
            Decelerate();
            playingParticles = false;
        }
        else
        {
            Accelerate(input);

            if (!playingParticles)
            {
                particleEvent = new ParticleEvent();
                particleEvent.particles = walkingParticles;
                particleEvent.objectPlaying = owner.gameObject;
                EventSystem.Current.FireEvent(particleEvent);

                playingParticles = !playingParticles;
            }

        }

    }

    void Speed()
    {
        speed = Input.GetAxis("Vertical");
        direction = Input.GetAxis("Horizontal");

        anim.SetFloat("speed", speed);
        anim.SetFloat("direction", direction);
    }


}

