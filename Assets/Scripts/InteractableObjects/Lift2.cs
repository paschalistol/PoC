//Main Author: Emil Dahl
//Secondary Author: Paschalis Tolios, Johan Ekman
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lift2 : MonoBehaviour
{

    private GameObject[] liftPoints;
    private int currentPoint;
    public Vector3 direction;
    public bool onOff = false;
    public GameObject audioMachine;
    private Vector3 velocity, startVelocity;
    [SerializeField] private float speed = 1;
    
    
    private void Start()
    {
        liftPoints = GetComponent<LiftPoints>().GetPoints();
        currentPoint = 0;
        startVelocity = velocity;
    }

    public Vector3 GetVelocity()
    {
        return velocity;
    }
    private void Update()
    {
        if (!GameController.isPaused)
        {
            if (onOff)
            {

                direction = liftPoints[currentPoint].transform.position - transform.position;
                velocity = direction.normalized * speed;

                transform.position += velocity * Time.deltaTime;

                if (Vector3.Distance(transform.position, liftPoints[currentPoint].transform.position) < 0.1f)
                {
                    onOff = false;
                    currentPoint = (currentPoint + 1) % liftPoints.Length;
                }
            }
            if (!onOff)
            {
                velocity = Vector3.zero;
            }
        }
    }
    public void ActivateLift() {
        onOff = true;
    }
}
