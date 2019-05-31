//Main Author: Emil Dahl
//Secondary Author: Paschalis Tolios

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuseBoxItem : Interactable
{
    protected const float skinWidth = 0.2f;
    //[SerializeField] private GameObject lockedDoor;
    [SerializeField] private GameObject fuseBox;
    //[SerializeField]private int itemQuantity = 2;
    //private static int count;
    [SerializeField] private LayerMask environment;
    [SerializeField] private AudioClip clip;

    private SoundEvent sound;
    //[SerializeField] private GameObject particles;
    //[SerializeField] private GameObject endParticles;


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

            RaycastHit raycastHit;

            Physics.BoxCast(transform.position, transform.localScale, transform.forward, out raycastHit, transform.rotation, transform.localScale.z);

            if (raycastHit.collider != null && raycastHit.collider.transform.gameObject == fuseBox)
            {

                sound = new SoundEvent();
                sound.audioClip = clip;
                sound.eventDescription = "Water sound";
                sound.looped = false;

                EventSystem.Current.FireEvent(sound);
                //fuseBox.GetComponent<FuseBox>().;
                fuseBox.GetComponent<FuseBox>().RunInteraction();

                //FuseBoxEvent fuseBoxEvent = new FuseBoxEvent();
                //fuseBoxEvent.gameObject = gameObject;
                //fuseBoxEvent.eventDescription = "Fusebox item: " + count;
                ////fuseBoxEvent.particles = particles;

                //EventSystem.Current.FireEvent(fuseBoxEvent);
                Destroy(gameObject);
              
              
            }
        }
    }
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
