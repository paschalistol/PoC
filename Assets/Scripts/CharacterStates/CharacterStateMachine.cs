﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterStateMachine : StateMachine
{
    public GameObject lift2;
    public LayerMask environment;
    public LayerMask deadlyEnvironment;
    public LayerMask pickups;
    public LayerMask lift;
    [HideInInspector]public float WobbleFactor = 0.15f;
 
    public float test;
    [HideInInspector] public Vector3 velocity;
    [HideInInspector] public float maxSpeed;

    public float GetMaxSpeed()
    {
        return maxSpeed;
    }
    protected override void Awake()
    {
        base.Awake();
    }

   
}
