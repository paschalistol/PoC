﻿
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyDog : StateMachine
{
    // Attributes
    [HideInInspector] public MeshRenderer Renderer;
    [HideInInspector] public NavMeshAgent agent;
    public LayerMask visionMask;
    public Character player;
    public Oldboi oldboi;
  
    [SerializeField] private float smellDistance;

    // Methods
    protected override void Awake()
    {
        Renderer = GetComponent<MeshRenderer>();
        agent = GetComponent<NavMeshAgent>();
      
        base.Awake();
    }

    public float GetSmellDistance()
    {
        return smellDistance;
    }

    public void SwitchToFollow(Vector3 position)
    {
        //ChangeState<DogFetchState>();
        agent.SetDestination(position);
        Debug.Log(agent.destination);
    }
}
