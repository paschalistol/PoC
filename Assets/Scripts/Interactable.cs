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
