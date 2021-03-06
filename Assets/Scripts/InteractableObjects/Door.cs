﻿//Author: Paschalis Tolios

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Interactable
{
    [SerializeField, Tooltip("Use negative speed to rotate in opposite direction")]
    private int rotationSpeed = 40;
    [SerializeField, Range(0f, 90f)]
    private int rotationGoal = 90;
    [SerializeField, Tooltip("Can the player open the door or is a key needed?")] private bool keyNeeded;
    [SerializeField, Tooltip("The text of the warning when trying to open a locked door")] private string warningText = "A key is needed to unlock the door";
    private GameObject parent;
    public bool used = false;
    [Header("Sounds")]
    [SerializeField] private AudioClip doorOpenSound;
    [SerializeField] private AudioClip doorCloseSound;
    [SerializeField] private AudioClip unlockDoorSound;
    float rotationCounter, perFrame;
    private SoundEvent soundEvent;
    private WarningEvent warningEvent;
    protected override void Start()
    {
        base.Start();
        warningEvent = new WarningEvent();
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
        if (keyNeeded == false)
        {
            if (used == false)
            {

                if (rotationGoal < 0)
                {
                    rotationSpeed *= -1;
                }
                soundEvent = new SoundEvent();

                soundEvent.eventDescription = "PickUp Sound";
                soundEvent.audioClip = doorOpenSound;
                soundEvent.looped = false;
                if (soundEvent.audioClip != null)
                {
                    EventSystem.Current.FireEvent(soundEvent);
                }
                StartCoroutine(RotateDoor(parent));
                used = true;
            }
            if (gameObject.GetComponent<ActiveGold>() != null)
            {
                gameObject.GetComponent<ActiveGold>().SetGoldActive();
            }
        }
        else if (keyNeeded)
        {
            warningEvent.warning = warningText;
            EventSystem.Current.FireEvent(warningEvent);
        }
    }
    public void UnlockDoor()
    {
        keyNeeded = false;
        soundEvent = new SoundEvent();

        soundEvent.eventDescription = "PickUp Sound";
        soundEvent.audioClip = unlockDoorSound;
        soundEvent.looped = false;
        if (soundEvent.audioClip != null)
        {
            EventSystem.Current.FireEvent(soundEvent);
        }

    }

    IEnumerator RotateDoor(GameObject parent)
    {
        rotationCounter = 0;
        while (rotationCounter < rotationGoal)
        {
            perFrame = Time.deltaTime * rotationSpeed;
            parent.transform.eulerAngles += new Vector3(0, 1, 0) * perFrame;
            rotationCounter += Mathf.Abs(perFrame);
            yield return null;
        }
    }
}
