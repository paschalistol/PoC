using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransfer : MonoBehaviour
{
    [SerializeField]
    private int levelToLoad = 1;
    public void ChangeLevel()
    {
        GameObject parent = transform.parent.gameObject;
        SceneManager.LoadScene("Level 3");

    }
}
