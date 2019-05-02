using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField]private GameObject lockedDoor;
    private PhysicsScript body;
    [HideInInspector]public bool used = false;

    protected Vector3 velocity;
    protected CapsuleCollider capsuleCollider;
    private PhysicsScript whereAreMyDragons;
    protected const float skinWidth = 0.2f;

   // protected bool usingGravity;
    protected bool isHeld;


    // Start is called before the first frame update
    void Start()
    {
        body = gameObject.GetComponent<PhysicsScript>();
        //usingGravity = false;
    }

    /**
     * Interaction: lyssna efter om vi försöker plocka upp nyckeln
     * Interaction: ändra till isHeld
     * Om den inte är "på" spelaren eller fienden - Run Gravity
     * 
     * **/
    void Update()
    {
        //if(transform.parent == null)
        //{
        //   usingGravity= true;
        //}

        if (!isHeld || transform.parent != null)
        {
            velocity = body.Gravity(velocity);
            velocity = body.CapsuleCollisionCheck(velocity, capsuleCollider, skinWidth);
            velocity = body.Decelerate(velocity);
            transform.position += velocity * Time.deltaTime;
        }

        RaycastHit raycastHit;
        bool boxCast = Physics.BoxCast(transform.position, transform.localScale, Vector3.forward, out raycastHit, transform.rotation, transform.localScale.z + 3f);
        if (Input.GetKeyDown(KeyCode.E) && raycastHit.collider != null && raycastHit.collider.transform.gameObject == lockedDoor && !used)
        {
            gameObject.SetActive(false);
            UnlockEvent interactedInfo = new UnlockEvent();
            interactedInfo.eventDescription = "The door has been unlocked!";
            interactedInfo.doorObject = raycastHit.collider.transform.gameObject;

            EventSystem.Current.FireEvent(interactedInfo);
            used = true;
        }
    }
    public void TakeKeyInteraction()
    {
        Debug.Log("Took key!");
        transform.parent = null;
    }

}
