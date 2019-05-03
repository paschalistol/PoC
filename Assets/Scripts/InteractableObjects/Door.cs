using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField]
    private int rotationSpeed = 40, rotation=90;
    public void InteractWithDoor()
    {
        GameObject parent = transform.parent.gameObject;
        StartCoroutine(RotateDoor(parent));
        
    }
    IEnumerator RotateDoor(GameObject parent)
    {
        float temp = parent.transform.eulerAngles.y + rotation;
        while (parent.transform.eulerAngles.y <temp)
        {
            parent.transform.eulerAngles += new Vector3(0, 1, 0) *Time.deltaTime * rotationSpeed;

            yield return null;
        }
    }
}
