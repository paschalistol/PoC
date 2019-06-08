//Main Author: Emil Dahl


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class DogBaseState : State
{
    // Attributes
    [SerializeField] protected Material material;
    [SerializeField] protected float moveSpeed;
    private BoxCollider boxCollider;
    protected EnemyDog owner;
    private UnitDeathEventInfo deathInfo;
    private SoundEvent sound;
    private StopSoundEvent stopSound;


    public override void EnterState()
    {
        base.EnterState();
        owner.agent.speed = moveSpeed;
        boxCollider = owner.GetComponent<BoxCollider>();

        sound = new SoundEvent();
        sound.parent = owner.gameObject;
    }

    public override void InitializeState(StateMachine owner)
    {
        this.owner = (EnemyDog)owner;
    }


    protected void KillPlayer()
    {
        deathInfo = new UnitDeathEventInfo();
        deathInfo.eventDescription = "You have been killed!";
        deathInfo.spawnPoint = owner.player.GetComponent<CharacterStateMachine>().currentCheckPoint;
        deathInfo.deadUnit = owner.player.transform.gameObject;
        EventSystem.Current.FireEvent(deathInfo);
    }

  
    #region DogBaseLegacy

    //protected bool InSafeZoneCheck()
    //{
    //    RaycastHit raycastHit;
    //    #region Raycast

    //    Physics.BoxCast(boxCollider.transform.position, boxCollider.transform.localScale / 2,
    //        owner.agent.velocity, out raycastHit, boxCollider.transform.rotation, owner.agent.velocity.magnitude * Time.deltaTime + skinWidth, owner.safeZoneMask);
    //    #endregion
    //    if(raycastHit.collider != null)
    //    {
    //        return true;
    //    }
    //    else
    //    {
    //        return false;
    //    }
        
    //}

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
