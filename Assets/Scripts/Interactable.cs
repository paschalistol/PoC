using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    
    protected Vector3 startPosition;
    protected virtual void Start()
    {
        startPosition = transform.position;
    }
    public abstract void StartInteraction();
    public virtual void BeingThrown(Vector3 throwDirection) {}
    public virtual void RespawnItem()
    {

        transform.position = startPosition;
    }
    public abstract AudioClip GetAudioClip();
    protected virtual void OnCollisionStay(Collision collision)
    {

        if (gameObject.CompareTag("Only Interaction") == false && collision.gameObject.layer != 0)
        {
            RespawnItem();
        }

    }

}
