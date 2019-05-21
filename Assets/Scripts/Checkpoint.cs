﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [Tooltip("Higher is better and should come later in the game")]
    [SerializeField] private int checkpointNo;
    private static int currentCheckpoint = 0;
    [Tooltip("Select the position where the player should revive at when going through the checkpoint")]
    [SerializeField] private GameObject restartPosition;
    private void Awake()
    {
        currentCheckpoint = 0;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") )
        {
                currentCheckpoint = checkpointNo;
            other.GetComponent<CharacterStateMachine>().currentCheckPoint = restartPosition;
        }
    }
}
