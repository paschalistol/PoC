﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KillZone : MonoBehaviour
{
    

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "player") 
        {
           
            SceneManager.LoadScene("test");
            
        }

        
    }

}
    