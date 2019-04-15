using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStateMachine : StateMachine
{
    public LayerMask environment;
    public LayerMask pickups;
    public LayerMask lift;
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
