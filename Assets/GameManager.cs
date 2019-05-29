using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;
    public List<GameObject> interactables = new List<GameObject>();
    private void Awake()
    {
        gameManager = this;
    }
}
