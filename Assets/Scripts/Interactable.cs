//Author: Paschalis Tolios
//Secondary Author: Johan Ekman
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{

    protected Vector3 StartPosition, Velocity, StartRotation, wallCollisionRespawn, wallCollisionRotation;
    private float timer;
    protected bool isHeld = false;
    protected int layerNumber;
    private bool thisWasCarriedBeforeRespawn = false;
    protected RaycastHit raycastHit;
    protected RaycastHit wallHit;
    protected BoxCollider boxCollider;
    [SerializeField]private int activeCollisions;
    protected virtual void Start()
    {
        StartPosition = transform.position;
        StartRotation = transform.eulerAngles;
        layerNumber = gameObject.layer;
        boxCollider = GetComponent<BoxCollider>();
    }
    public virtual bool IsHeld()
    {
        return isHeld;
    }
    public virtual void StartInteraction()
    {
        
    }
    public virtual void BeingThrown(Vector3 throwDirection) { }
    public virtual void RespawnItem()
    {
        Velocity = Vector3.zero;
        transform.position = StartPosition;
        transform.eulerAngles = StartRotation;
    }

    public abstract AudioClip GetAudioClip();

    protected virtual void OnTriggerEnter(Collider other)
    {
        activeCollisions++;
        if (other.CompareTag("Player") == false && gameObject.CompareTag("Only Interaction") == false && other.gameObject.layer != 0)
        {
           
            GetMovementDirection();
            wallCollisionRotation = transform.eulerAngles;

            thisWasCarriedBeforeRespawn = true;

            Velocity = Vector3.zero;
        }
    }
    private void GetMovementDirection()
    {

        if (wallHit.collider != null)
        {

            wallCollisionRespawn =  wallHit.normal;
            wallCollisionRespawn.y = 0;
            wallCollisionRespawn = wallCollisionRespawn.normalized;

        }
    } 

    protected virtual void OnTriggerStay(Collider other)
    {
        if (other != gameObject &&  thisWasCarriedBeforeRespawn && (other.CompareTag("Untagged") == true || other.CompareTag("Environment") == true || other.CompareTag("Only Interaction") == true || other.CompareTag("Box") == true || other.CompareTag("Player") == true || other.CompareTag("Door") == true || other.CompareTag("FuseBoxItem") == true || other.CompareTag("Trampoline") == true || other.CompareTag("ElectricalDoor") == true || other.CompareTag("Key") == true || other.CompareTag("Battery") == true) && other.gameObject.layer != 0 && !(gameObject.CompareTag("Only Interaction") || gameObject.CompareTag("Valuables")))
        {
            if (isHeld == false || (timer > 0.5f))
            {
                WallCollisionRespawn();
            }
            timer += Time.deltaTime;
        }
        else
        {
            thisWasCarriedBeforeRespawn = false;
        }

    }

    protected virtual void WallCollisionRespawn()
    {


        transform.position += (wallCollisionRespawn).normalized;
        isHeld = false;
        gameObject.layer = layerNumber;
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        activeCollisions--;
        if (activeCollisions ==0)
        {

        thisWasCarriedBeforeRespawn = false;
        }
        Velocity = Vector3.zero;
        timer = 0;
        
    }
    public virtual void SetVelocity(Vector3 velocity)
    {

        Velocity = velocity;
    }
    public virtual Vector3 GetVelocity()
    {
        return Velocity;
    }


    protected Vector3 LookDirection()
    {
        return -Camera.main.GetComponent<CameraScript>().getRelationship();
    }

    public virtual void RotateAround(Transform owner)
    {
        transform.RotateAround(owner.position, Vector3.up, owner.eulerAngles.y - transform.eulerAngles.y);

    }

    protected void GetWallNormal()
    {

        Physics.BoxCast(boxCollider.transform.position, boxCollider.transform.localScale / 2,
            (new Vector3(LookDirection().z, 0, -LookDirection().x) * Input.GetAxis("Mouse X")).normalized, out raycastHit, boxCollider.transform.rotation, 1f);
        if (raycastHit.collider != null && raycastHit.collider.gameObject.CompareTag("Player") == false)
        {

            wallHit = raycastHit;
        }
        Debug.DrawRay(transform.position, (new Vector3(LookDirection().z, 0, -LookDirection().x) * Input.GetAxis("Mouse X")).normalized * 5f, Color.green);
        // Debug.Log((new Vector3(LookDirection().z, 0, -LookDirection().x) * Input.GetAxis("Mouse X")).normalized);

    }


    /*protected virtual void SavePlayer()
{
    SaveSystem.SavePlayer(this);
}
protected virtual void LoadPlayer()
{
    PlayerData data = SaveSystem.LoadPlayer();
    Vector3 savedPosition;
    savedPosition.x = data.position[0];
    savedPosition.y = data.position[1];
    savedPosition.z = data.position[2];
    transform.position = savedPosition;
}*/
}
