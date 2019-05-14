//Author: Paschalis Tolios
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Interactable
{
    [SerializeField]
    private int rotationSpeed = 40, rotation=90;
    private GameObject parent;

    [Header("Sounds")]
    [SerializeField]
    private AudioClip doorOpenSound, doorCloseSound, unlockDoorSound;
    public override AudioClip GetAudioClip()
    {
        return doorOpenSound;
    }

    private void Awake()
    {
        parent = transform.parent.gameObject;
    }

    public override void StartInteraction()
    {
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
