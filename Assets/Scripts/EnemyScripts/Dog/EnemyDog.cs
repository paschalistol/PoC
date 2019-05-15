//Main Author: Emil Dahl
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyDog : StateMachine
{
    // Attributes
    [HideInInspector] public MeshRenderer Renderer;
    [HideInInspector] public NavMeshAgent agent;
    [SerializeField]public LayerMask visionMask { get; set; }
    [SerializeField]public LayerMask safeZoneMask { get; set; }
    [SerializeField]public GameObject player { get; set; }
    [SerializeField]public Oldboi oldboi { get; set; }

   // public GameObject audioSpeaker;

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
}


