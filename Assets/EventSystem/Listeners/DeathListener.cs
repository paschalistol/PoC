using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathListener : MonoBehaviour
{
    private GameObject objectToInteract;
    public GameObject checkPoint;
    public GameObject player;
    void Start()
    {

        EventSystem.Current.RegisterListener<UnitDeathEventInfo>(DeathInteraction);
    }

    void DeathInteraction(UnitDeathEventInfo deathInfo)
    {

        objectToInteract = deathInfo.deadUnit;
        Debug.Log("1");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        
        //player.gameObject.transform.position = spawn.transform.position;
    }
}
