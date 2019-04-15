using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolPoints : MonoBehaviour
{
    [SerializeField]private GameObject[] points;

    public GameObject[] GetPoints()
    {
        return points;
    }
}
