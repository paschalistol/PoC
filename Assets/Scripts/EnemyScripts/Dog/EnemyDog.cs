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
    private Vector3 startPosition, startRotation;
    // public GameObject audioSpeaker;

    [SerializeField] private float smellDistance;

    // Methods
    protected override void Awake()
    {
        Renderer = GetComponent<MeshRenderer>();
        agent = GetComponent<NavMeshAgent>();
        startPosition = transform.position;
        startRotation = transform.eulerAngles;
        base.Awake();
    }

    public float GetSmellDistance()
    {
        return smellDistance;
    }

    public void ResetTransform()
    {
        transform.position = startPosition;
        transform.eulerAngles = startRotation;
        Debug.Log(startPosition);
    }

}


