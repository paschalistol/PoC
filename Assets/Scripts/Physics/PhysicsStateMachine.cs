using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsStateMachine : StateMachine
{
    public LayerMask environment;
    [HideInInspector] public Vector3 velocity;

    protected override void Awake()
    {
        base.Awake();
    }



}
