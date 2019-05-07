//Main Author: Paschalis Tolios
//Secondary Author: Emil Dahl

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldboiPatrolPoints : MonoBehaviour
{
    [SerializeField]private GameObject[] points;

    public GameObject[] GetPoints()
    {
        return points;
    }
}
