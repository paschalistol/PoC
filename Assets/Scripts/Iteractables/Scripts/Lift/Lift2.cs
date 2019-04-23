using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lift2 : MonoBehaviour
{

    private GameObject[] bigPoints;
    private int currentPoint;
    public Vector3 direction;
    [HideInInspector]public bool onOff = false;
    public GameObject audioMachine;


    private void Start()
    {
        bigPoints = GetComponent<LiftPoints>().GetPoints();
        currentPoint = 0;
    }


    private void Update()
    {

        if (onOff)
        {
            direction = bigPoints[currentPoint].transform.position - transform.position;

            transform.position += direction.normalized * 1f * Time.deltaTime;

            if (Vector3.Distance(transform.position, bigPoints[currentPoint].transform.position) < 1)
            {
                currentPoint = (currentPoint + 1) % bigPoints.Length;

            }
        }
    }
}
