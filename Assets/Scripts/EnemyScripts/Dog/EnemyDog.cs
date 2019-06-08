//Main Author: Emil Dahl
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyDog : StateMachine
{
    [SerializeField] private float smellDistance;

    [HideInInspector] public MeshRenderer Renderer;
    [HideInInspector] public NavMeshAgent agent;
    [HideInInspector] public bool isInChase;
    [HideInInspector] public bool inSafeZone;

    public LayerMask safeZoneMask;
    public GameObject player;
    private Vector3 startPosition, startRotation;



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
    }

}


