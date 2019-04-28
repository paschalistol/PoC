using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public GameObject lockedDoor;
    private Rigidbody body;
    [HideInInspector]public bool used = false;


    // Start is called before the first frame update
    void Start()
    {
        body = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.parent == null)
        {
           body.isKinematic = false;
           body.useGravity = true;
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


        //Vector3 temp = new Vector3(transform.position.x, 1.3f, transform.position.z);
        //transform.position = temp;
        //if (carried)
        //{
        //    Vector3 temp = new Vector3(transform.position.x, 1.3f, transform.position.z);
        //    transform.position = temp;
        //}
        //Debug.Log(carried);
    }

    //public void TakeKeyInteraction()
    //{
    //    Debug.Log("Took key!");
    //    transform.parent = null;
    //}

}
