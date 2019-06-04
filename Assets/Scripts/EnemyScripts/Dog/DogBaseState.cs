//Main Author: Emil Dahl


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class DogBaseState : State
{
    // Attributes
    [SerializeField] protected Material material;
    private RaycastHit raycastHit;
    private BoxCollider boxCollider;
    protected EnemyDog owner;
    private UnitDeathEventInfo deathInfo;

    [SerializeField] protected float moveSpeed;
    private const float skinWidth = 0.2f;
    private float fieldOfView;
    private Vector3 scale;
    private Vector3 bigScale;
    private SoundEvent sound;
    private StopSoundEvent stopSound;
    
 
    

    // Methods
    public override void EnterState()
    {
        base.EnterState();
        //owner.Renderer.material = material;
        owner.agent.speed = moveSpeed;
        boxCollider = owner.GetComponent<BoxCollider>();

        sound = new SoundEvent();
        sound.parent = owner.gameObject;

        stopSound = new StopSoundEvent();
     
        
    }

    public override void InitializeState(StateMachine owner)
    {
        this.owner = (EnemyDog)owner;
    }

    protected bool InSafeZoneCheck()
    {
        RaycastHit raycastHit;
        #region Raycast

        Physics.BoxCast(boxCollider.transform.position, boxCollider.transform.localScale / 2,
            owner.agent.velocity, out raycastHit, boxCollider.transform.rotation, owner.agent.velocity.magnitude * Time.deltaTime + skinWidth, owner.safeZoneMask);
        #endregion
        if(raycastHit.collider != null)
        {
            return true;
        }
        else
        {
            return false;
        }
        
    }

    protected void KillPlayer()
    {
        deathInfo = new UnitDeathEventInfo();
        deathInfo.eventDescription = "You have been killed!";
        deathInfo.spawnPoint = owner.player.GetComponent<CharacterStateMachine>().currentCheckPoint;
        deathInfo.deadUnit = owner.player.transform.gameObject;
        EventSystem.Current.FireEvent(deathInfo);
    }

    protected void StartDogSound(AudioClip barkSound, bool loopedOrNot)
    {
        sound.audioClip = barkSound;
        sound.looped = loopedOrNot;
        EventSystem.Current.FireEvent(sound);
    }

    protected void StopDogSound()
    {
        stopSound.AudioPlayer = sound.objectInstatiated;
        EventSystem.Current.FireEvent(stopSound);
    }



    #region legacy


    //fieldOfView = owner.GetFieldOfView();
    /* protected bool LineOfSight()
     {
         //Debug.Log(owner.agent.velocity.normalized);
         Debug.DrawRay(owner.agent.transform.position, owner.agent.velocity, Color.red, 0);
         return Physics.CapsuleCast(owner.transform.position + capsuleCollider.center + Vector3.up * (capsuleCollider.height / 2 - capsuleCollider.radius), owner.transform.position + capsuleCollider.center + Vector3.down * (capsuleCollider.height / 2 - capsuleCollider.radius),
            capsuleCollider.radius, owner.agent.velocity, out RaycastHit raycastHit, fieldOfView, owner.visionMask);

     }*/
    #endregion
}
