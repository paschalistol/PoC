using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathListener : MonoBehaviour
{
    private GameObject objectToInteract;
    private GameObject spawnPoint;
    public GameObject player;
    void Start()
    {

        EventSystem.Current.RegisterListener<UnitDeathEventInfo>(DeathInteraction);
    }

    void DeathInteraction(UnitDeathEventInfo deathInfo)
    {

        objectToInteract = deathInfo.deadUnit;
        spawnPoint = deathInfo.spawnPoint;
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        
        player.gameObject.transform.position = spawnPoint.transform.position;
    }
}
