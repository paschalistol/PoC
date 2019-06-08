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
    public LayerMask visionMask;
    public GameObject player;
    public GameObject flashLight;
    public CapsuleCollider capsuleCollider;
    public GameObject[] dogs;

    private Vector3 startPosition, startRotation;

    // Methods
    protected override void Awake()
    {
        Renderer = GetComponent<MeshRenderer>();
        agent = GetComponent<NavMeshAgent>();
        startPosition = transform.position;
        startRotation = transform.eulerAngles;
        base.Awake();
    }

    public float GetFieldOfView()
    {
        return fieldOfView;
    }

    public void ResetTransform()
    {
        transform.position = startPosition;
        transform.eulerAngles = startRotation;
    }
}
