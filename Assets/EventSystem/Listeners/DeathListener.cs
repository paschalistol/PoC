using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathListener : MonoBehaviour
{
    private GameObject objectToInteract;
    void Start()
    {

        EventSystem.Current.RegisterListener<UnitDeathEventInfo>(DeathInteraction);
    }

    void DeathInteraction(UnitDeathEventInfo deathInfo)
    {

        objectToInteract = deathInfo.deadUnit;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); 
    }
}
