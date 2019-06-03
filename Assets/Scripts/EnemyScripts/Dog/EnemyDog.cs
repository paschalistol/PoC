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
    [HideInInspector] public bool isInChase;
    //public LayerMask visionMask;
    public LayerMask safeZoneMask;
    public GameObject player;
    

    public bool inSafeZone;
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


