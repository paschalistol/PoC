//Main Author: Emil Dahl
//Secondary Author: Paschalis Tolios

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class handles the behavior for items used on FuseBoxes
/// </summary>
public class FuseBoxItem : Interactable
{
    [SerializeField] private GameObject fuseBox;
    [SerializeField] private LayerMask environment;
    [SerializeField] private AudioClip clip;
    [SerializeField] private float volume;
    protected const float skinWidth = 0.2f;

    private SoundEvent sound;


    protected override void Start()
    {
        base.Start();
        boxCollider = GetComponent<BoxCollider>();
        isHeld = false;
    }

    void Update()
    {

        if (!GameController.isPaused)
        {
            AddPhysics();
            transform.position += Velocity * Time.deltaTime;
            DoBoxCast(transform.forward);
            DoBoxCast(transform.right);
            DoBoxCast(transform.right * -1);

        }
    }

    /// <summary>
    /// BoxCast to check if there is something to activate in front of the object
    /// </summary>
    /// <param name="direction"></param>
    private void DoBoxCast(Vector3 direction)
    {
        Physics.BoxCast(transform.position, transform.localScale, direction, out raycastHit, transform.rotation, transform.localScale.z);

        if (raycastHit.collider != null && raycastHit.collider.transform.gameObject == fuseBox)
        {
            sound = new SoundEvent();
            sound.audioClip = clip;
            sound.eventDescription = "Water sound";
            sound.looped = false;
            sound.parent = Camera.main.gameObject;
            sound.volume = volume;

            EventSystem.Current.FireEvent(sound);
            fuseBox.GetComponent<FuseBox>().RunInteraction();


            gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Activates gravity and deceleration on set object if not held, and checks for collision all the time
    /// </summary>
    private void AddPhysics()
    {
        if (!isHeld)
        {
            Velocity = PhysicsScript.Decelerate(Velocity);
            Velocity = PhysicsScript.Gravity(Velocity);
        }
        else
        {
            GetWallNormal();
        }
        Velocity = PhysicsScript.CollisionCheck(Velocity, boxCollider, skinWidth, environment);

    }

    /// <summary>
    /// Changes the item from held to not held, or not held to held
    /// </summary>
    public override void StartInteraction()
    {
        base.StartInteraction();
        isHeld = !isHeld;
    }

    public override AudioClip GetAudioClip()
    {
        return null;
    }
}
#region FuseBoxItemLegacy
//FuseBoxEvent fuseBoxEvent = new FuseBoxEvent();
//fuseBoxEvent.gameObject = gameObject;
//fuseBoxEvent.eventDescription = "Fusebox item: " + count;
////fuseBoxEvent.particles = particles;
//fuseBox.GetComponent<FuseBox>().;

//EventSystem.Current.FireEvent(fuseBoxEvent);
//[SerializeField] private GameObject particles;
//[SerializeField] private GameObject endParticles;
//[SerializeField] private GameObject lockedDoor;
//[SerializeField]private int itemQuantity = 2;
//private static int count;

#endregion
