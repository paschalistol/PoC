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
    [SerializeField] private AudioClip groundedSound;
    [SerializeField] private float dynamicFrictionCoeff = 0.35f, maxSpeedCoeff = 10;
    public override void EnterState()
    {
        base.EnterState();

        dynamicFriction = dynamicFrictionCoeff;
        MaxSpeed = maxSpeedCoeff;
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
        Gravity();
        #region Input
        Vector3 input = GetDirectionInput();


        if (input.magnitude <= 0)
        {
            Decelerate();
        }
        else
        {
            Accelerate(input);
        }
        #endregion
        ChangeCharRotation();

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
            MaxSpeed /= 2;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            MaxSpeed *= 2;
        }
        #endregion

        DeathCollisionCheck();
        ReachingCheckPoint();
        Trampoline();
        CollisionCheck();
        //ReachingGoal();
        owner.transform.position += Velocity * Time.deltaTime;


        if (IsGliding())
        {
            owner.ChangeState<GlidingState>();
        }

        if(TakingLift2() != null)
        {
            owner.ChangeState<OnLiftState>();
        }
        if (!IsGrounded())
        {
            owner.ChangeState<InTheAirState>();

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

