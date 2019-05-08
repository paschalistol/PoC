//Main Author: Emil Dahl
//Secondary Author: Paschalis Tolios
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : StateMachine
{
    // Attributes
    [HideInInspector] public MeshRenderer Renderer;
    [HideInInspector] public NavMeshAgent agent;
    private float fieldOfView { get; }
    [SerializeField] private float hearingDistance;
    public LayerMask visionMask;
    public LayerMask ignoreLayerMask;
    public GameObject player; //character?
    public GameObject flashLight;
    public CapsuleCollider capsuleCollider;
    public readonly float LightThreshold = 0.4f;

    // Methods
    protected override void Awake()
    {
        Renderer = GetComponent<MeshRenderer>();
        agent = GetComponent<NavMeshAgent>();
        base.Awake();
    }

    public float GetFieldOfView()
    {
        return fieldOfView;
    }

    public float GetHearingDistance()
    {
        return hearingDistance;
    }
}
