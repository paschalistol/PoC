
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
   // public GameObject flashLight;
   // [SerializeField] private float fieldOfView;
   // [SerializeField] private float hearingDistance;
    [SerializeField] private float smellDistance;

    // Methods
    protected override void Awake()
    {
        Renderer = GetComponent<MeshRenderer>();
        agent = GetComponent<NavMeshAgent>();
      //  flashLight = GetComponent<FlashLight>();
        base.Awake();
    }
    /*  public float GetFieldOfView()
      {
          return fieldOfView;
      }

      public float GetHearingDistance()
      {
          return hearingDistance;
      }*/

    public float GetSmellDistance()
    {
        return smellDistance;
    }
}
