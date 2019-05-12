using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnItem : MonoBehaviour
{
    private Vector3 startPosition;

    public void Start()
    {
        startPosition = transform.position;
    }

    public void Respawn()
    {
        transform.position = startPosition;
    }
}
