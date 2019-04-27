using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHoldItemStateMachine : StateMachine
{
    public LayerMask pickups;
    [HideInInspector] public bool holdingSth;


    protected override void Awake()
    {
        base.Awake();
    }
}
