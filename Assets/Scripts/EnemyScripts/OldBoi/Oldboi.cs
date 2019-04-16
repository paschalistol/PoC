
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
    public Character player;
    public EnemyDog doggo;
   
    //public GameObject flashLight;
    public CapsuleCollider capsuleCollider;

    // Methods
    protected override void Awake()
    {
        Renderer = GetComponent<MeshRenderer>();
        agent = GetComponent<NavMeshAgent>();
      //  flashLight = GetComponent<FlashLight>();
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
