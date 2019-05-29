using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Gameobjects
    public static GameManager gameManager;
    public List<GameObject> interactables = new List<GameObject>();
    public List<GameObject> enemies = new List<GameObject>();
    public GameObject player;
    #endregion
    private void Awake()
    {
        gameManager = this;
    }
}
