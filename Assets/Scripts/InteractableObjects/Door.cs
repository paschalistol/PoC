using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField]
    private int rotationSpeed = 40;
    public void InteractWithDoor()
    {
        GameObject parent = transform.parent.gameObject;
        StartCoroutine(RotateDoor(parent));
        
    }
    IEnumerator RotateDoor(GameObject parent)
    {
        while (parent.transform.eulerAngles.y <90)
        {
            parent.transform.eulerAngles += new Vector3(0, 1, 0) *Time.deltaTime * rotationSpeed;

            yield return null;
        }
    }
}
