//Author: Paschalis Tolios

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterStateMachine : StateMachine
{
    public GameObject lift2;
    public GameObject currentCheckPoint;
    public LayerMask environment;
    public LayerMask deadlyEnvironment;
    public LayerMask lift;
    public LayerMask checkPoint;
    public LayerMask trampoline;
    public LayerMask goal;
    public float WobbleFactor = 0.50f;
    public GameObject walkingParticles;
    public bool standOnTrampoline = false;
    public bool grounded = false; 
    [SerializeField] public float bounceHeight = 20;

    [HideInInspector] public Vector3 velocity;
    [HideInInspector] public float maxSpeed;

    public float GetMaxSpeed()
    {
        return maxSpeed;
    }
    protected override void Awake()
    {
        base.Awake();
    }

    public void SavePlayer()
    {
        
        SaveSystem.SavePlayer(this);
        
    }

    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();

        Vector3 savedPosition;
        savedPosition.x = data.position[0];
        savedPosition.y = data.position[1];
        savedPosition.z = data.position[2];

        transform.position = savedPosition;
    }

   
}
