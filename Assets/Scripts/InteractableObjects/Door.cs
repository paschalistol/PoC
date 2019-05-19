//Author: Paschalis Tolios

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Interactable
{
    [SerializeField]
    private int rotationSpeed = 40, rotation = 90;
    private GameObject parent;
    private bool used = false;
    [Header("Sounds")]
    [SerializeField] private AudioClip doorOpenSound;
    [SerializeField] private AudioClip doorCloseSound;
    [SerializeField] private AudioClip unlockDoorSound;
    private float rotationGoal;
    protected override void Start()
    {
        base.Start();
    }
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
        if (used == false)
        {
            StartCoroutine(RotateDoor(parent));
            used = true;
        }
    }

    IEnumerator RotateDoor(GameObject parent)
    {
        rotationGoal = parent.transform.eulerAngles.y + rotation;
        while (parent.transform.eulerAngles.y < rotationGoal)
        {
            parent.transform.eulerAngles += new Vector3(0, 1, 0) * Time.deltaTime * rotationSpeed;

            yield return null;
        }
    }
}
