//Main Author: Emil Dahl
//Secondary Author: Paschalis Tolios, Johan Ekman
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lift2 : MonoBehaviour
{

    private GameObject[] bigPoints;
    private int currentPoint;
    public Vector3 direction;
    public bool onOff = false;
    public GameObject audioMachine;
    private Vector3 velocity;
    [SerializeField] private float speed = 1;

    
    private void Start()
    {
        bigPoints = GetComponent<LiftPoints>().GetPoints();
        currentPoint = 0;
     
    }

    public Vector3 GetVelocity()
    {
        return velocity;
    }
    private void Update()
    {

        if (onOff)
        {
            direction = bigPoints[currentPoint].transform.position - transform.position;
            velocity = direction.normalized  * speed ;

            transform.position += velocity * Time.deltaTime;

            if (Vector3.Distance(transform.position, bigPoints[currentPoint].transform.position) < 1)
            {
                onOff = false;
                currentPoint = (currentPoint + 1) % bigPoints.Length;
            }
        }
    }
    public void ActivateLift() {
        onOff = true;
    }
}
