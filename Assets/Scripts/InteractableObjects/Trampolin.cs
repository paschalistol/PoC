//Johan Ekman
//Secondary Author: Emil Dahl
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Trampolin : Interactable
{


    [HideInInspector] public bool used = false;

    [SerializeField] private LayerMask environment;
    [SerializeField] private LayerMask bounceLayer;
    protected const float skinWidth = 0.2f;
    public bool standOnTrampoline = false;
    protected float bounceHeight = 25;


    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        boxCollider = GetComponent<BoxCollider>();

        isHeld = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameController.isPaused)
        {
            
                transform.position += Velocity * Time.deltaTime;
            Bounce();
            Bouncing();
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

    public override AudioClip GetAudioClip()
    {
        return null;
    }

    public override void StartInteraction()
    {
        
        isHeld = !isHeld;
    }

    public void Bounce()
    {
        RaycastHit raycastHit;
        #region Raycast

        Physics.BoxCast(boxCollider.transform.position, boxCollider.transform.localScale / 2,
            Vector3.up, out raycastHit, boxCollider.transform.rotation, 0.5f, bounceLayer);
        #endregion
        if (raycastHit.collider == null)
            return;
        else
        {
            GameObject obj = raycastHit.collider.transform.gameObject;

            if (obj.tag == ("Player"))
            {
                System.String playerName = obj.name;
                CharacterStateMachine player = GameObject.Find(playerName).GetComponent<CharacterStateMachine>();
                player.standOnTrampoline = true;
            }

            if (obj.tag == ("Box"))
            {
                System.String boxName = obj.name;
                Box box = GameObject.Find(boxName).GetComponent<Box>();
                
                box.standOnTrampoline = true;
            }
            if (obj.tag == ("Trampoline") && obj != gameObject)
            {
                System.String trampolineName = obj.name;
                Trampolin trampoline = GameObject.Find(trampolineName).GetComponent<Trampolin>();
                trampoline.standOnTrampoline = true;
            }
        }
    }

    protected void Bouncing()
    {
        if (standOnTrampoline)
        {
            Velocity = new Vector3(Velocity.x * 1.18f, bounceHeight, Velocity.z * 1.18f);
            standOnTrampoline = false;
        }
    }

    public override void BeingThrown(Vector3 throwDirection)
    {
        Velocity = throwDirection;
    }



}
