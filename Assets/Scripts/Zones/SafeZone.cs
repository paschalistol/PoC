//Main Author: Emil Dahl
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeZone : MonoBehaviour
{
    void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.CompareTag("Dog"))
        {
            collision.transform.gameObject.GetComponent<EnemyDog>().inSafeZone = true;
            Debug.Log("in");
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
