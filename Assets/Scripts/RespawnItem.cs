using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnItem : MonoBehaviour
{
    private Vector3 startPosition;
    private const float respawnHeight = 20f;

    public void Start()
    {
        startPosition = transform.position;
    }

    public void Respawn()
    {
        Vector3 temp = new Vector3(0, respawnHeight, 0);
        transform.position = startPosition + temp;
    }
}
