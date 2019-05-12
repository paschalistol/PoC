using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnItem : MonoBehaviour
{
    [HideInInspector] public Vector3 startPosition;

    public void Respawn()
    {
        transform.position = startPosition;
    }
}
