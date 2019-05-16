using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    protected Vector3 startPosition;
    private void Start()
    {

    }
    public abstract void StartInteraction();
    public virtual void RespawnItem()
    {
        transform.position = startPosition;
    }
    public abstract AudioClip GetAudioClip();
}
