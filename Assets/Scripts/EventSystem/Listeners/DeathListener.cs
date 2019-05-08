//Main Author: Emil Dahl
//Secondary Author: Johan Ekman, Paschalis Tolios
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DeathListener : MonoBehaviour
{
    private GameObject objectToInteract;
    private GameObject spawnPoint;
    public GameObject player;
    public Text text;
    void Start()
    {

        EventSystem.Current.RegisterListener<UnitDeathEventInfo>(DeathInteraction);
        text.text = "" + PlayerPrefs.GetInt("DeathCounter");
    }

    void DeathInteraction(UnitDeathEventInfo deathInfo)
    {

        objectToInteract = deathInfo.deadUnit;
        spawnPoint = deathInfo.spawnPoint;
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        PlayerPrefs.SetInt("DeathCounter", PlayerPrefs.GetInt("DeathCounter") +1);
        text.text = ""+PlayerPrefs.GetInt("DeathCounter");
        player.gameObject.transform.position = spawnPoint.transform.position;
    }
}
