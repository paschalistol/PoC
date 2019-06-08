//Main Author: Emil Dahl
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class makes the dog run away if it enters the SafeZone
/// </summary>
public class SafeZone : MonoBehaviour
{
    void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.CompareTag("Dog"))
        {
            collision.transform.gameObject.GetComponent<EnemyDog>().inSafeZone = true;
        }
    }

    void OnTriggerExit(Collider collision)
    {
        if (collision.transform.CompareTag("Dog"))
        {
            collision.transform.gameObject.GetComponent<EnemyDog>().inSafeZone = false;
        }
    }
}
