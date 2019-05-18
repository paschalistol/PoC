using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampolin : Interactable
{


    [HideInInspector] public bool used = false;

    protected Vector3 velocity;
    protected BoxCollider boxCollider;
    [SerializeField] private LayerMask environment;
    [SerializeField] private LayerMask bounceLayer;
    protected const float skinWidth = 0.2f;
    

    private bool isHeld;
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
        if (!isHeld)
        {
            velocity = PhysicsScript.Decelerate(velocity);
            velocity = PhysicsScript.Gravity(velocity);
            velocity = PhysicsScript.CollisionCheck(velocity, boxCollider, skinWidth, environment);
            transform.position += velocity * Time.deltaTime;
        }
        Bounce();
    }

    public override AudioClip GetAudioClip()
    {
        return null;
    }

    public override void StartInteraction()
    {
        Debug.Log("StartingInteraction");
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
            if(raycastHit.collider.transform.gameObject.tag == ("player"))
            {
                CharacterStateMachine player = GameObject.Find("Player").GetComponent<CharacterStateMachine>();
                player.standOnTrampoline = true;
            }

            if (raycastHit.collider.transform.gameObject.tag == ("Box"))
            {
                Box box = GameObject.Find("Box").GetComponent<Box>();
                box.standOnTrampoline = true;
            }
        }
    }

}
