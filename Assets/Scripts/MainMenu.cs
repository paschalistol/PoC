﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
  public void Play()
    {
        SceneManager.LoadScene("test");
    }

    public void Controls()
    {
        SceneManager.LoadScene("Controls");
    }

    public void Quit()
    {
        UnityEditor.EditorApplication.isPlaying = false;
    }
}
