//Main Author: Emil Dahl

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Oldboi : StateMachine
{
    // Attributes
    [HideInInspector] public MeshRenderer Renderer;
    [HideInInspector] public NavMeshAgent agent;
    [SerializeField] private float fieldOfView;
    [SerializeField] private float hearingDistance;
    public LayerMask visionMask;
    public GameObject player;
    public GameObject[] dogs;
   
    public CapsuleCollider capsuleCollider;

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
#region OldboiLegacy 
//  flashLight = GetComponent<FlashLight>();
//public GameObject flashLight;
//public GameObject doggo;
// public Audio audioPlayer;
#endregion