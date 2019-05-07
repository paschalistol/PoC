﻿//Author: Paschalis Tolios
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    [SerializeField] private State[] states = null;
    private Dictionary<Type, State> stateDictionary = new Dictionary<Type, State>();
    public State currentState;


    protected virtual void Awake()
    {
        foreach (State state in states)
        {
            State instance = Instantiate(state);
            instance.InitializeState(this);
            stateDictionary.Add(instance.GetType(), instance);
            if (currentState == null)
            {
                currentState = instance;
            }
            currentState.EnterState();
        }
    }
    public void ChangeState<T>() where T : State
    {
        currentState.ExitState();
        currentState = stateDictionary[typeof(T)];
        currentState.EnterState();
    }
    private void Update()
    {
        currentState.ToDo();
    }
}
