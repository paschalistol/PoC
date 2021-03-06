﻿//Main Author: Paschalis Tolios
//Secondary author: Johan Ekman


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Character/InTheAir")]
public class InTheAirState : CharacterBaseState
{
    [SerializeField] private AudioClip jumpSound;
    public override void EnterState()
    {
        base.EnterState();
        owner.grounded = false;
        SoundEvent soundEvent = new SoundEvent();
        
        soundEvent.eventDescription = "Jump Sound";
        soundEvent.audioClip = jumpSound;
        soundEvent.looped = false;
        soundEvent.parent = owner.gameObject;
        if (soundEvent.audioClip != null)
        {
            EventSystem.Current.FireEvent(soundEvent);
        }

    }
    public override void ToDo()
    {
        if (!GameController.isPaused)
        {
            ChangeCharRotation();
            #region Input
            Vector3 input = GetDirectionInput() * 10;
            if (input.magnitude <= 0)
            {
                Decelerate();
            }
            else
            {
                Accelerate(input);
            }
            #endregion
            Gravity();
            CollisionCheck();
            DeathCollisionCheck();
            ReachingCheckPoint();
            //Trampoline();
            Bouncing();
            //ReachingGoal();

            owner.transform.position += Velocity * Time.deltaTime;
            if (IsGrounded())
            {

                owner.ChangeState<GroundedState>();
                owner.inAir = false;
            }

            
        }

    }

}
