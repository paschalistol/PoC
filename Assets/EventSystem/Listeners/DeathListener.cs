using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathListener : MonoBehaviour
{
    private GameObject objectToInteract;
    void Start()
    {
        Debug.Log("REEEEE");
        EventSystem.Current.RegisterListener<UnitDeathEventInfo>(DeathInteraction);
    }

    void DeathInteraction(UnitDeathEventInfo deathInfo)
    {
        Debug.Log("REEEEE2");
        objectToInteract = deathInfo.deadUnit;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); 
    }
}
