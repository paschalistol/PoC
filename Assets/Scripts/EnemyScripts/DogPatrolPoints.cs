using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogPatrolPoints : MonoBehaviour
{
    [SerializeField]private GameObject[] dogPoints;

    public GameObject[] GetPoints()
    {
        return dogPoints;
    }
}
