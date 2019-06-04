//Author: Paschalis Tolios

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHoldItemStateMachine : StateMachine
{
    public LayerMask Interactables;
    public GameObject ObjectCarried;
    [HideInInspector] public bool holdingSth;



    protected override void Awake()
    {
        base.Awake();
    }
}
