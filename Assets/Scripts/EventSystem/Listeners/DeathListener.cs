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
    public Text text;
    private static bool died;
    void Start()
    {

        EventSystem.Current.RegisterListener<UnitDeathEventInfo>(DeathInteraction);
        text.text = "" + PlayerPrefs.GetInt("DeathCounter");
    }

    void DeathInteraction(UnitDeathEventInfo deathInfo)
    {

        objectToInteract = deathInfo.deadUnit;
        //spawnPoint = deathInfo.spawnPoint;
        PlayerPrefs.SetInt("DeathCounter", PlayerPrefs.GetInt("DeathCounter") +1);
        text.text = ""+PlayerPrefs.GetInt("DeathCounter");
        DecreaseHighscore();
        died = true;
        //player.gameObject.transform.position = spawnPoint.transform.position;
       // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    void DecreaseHighscore()
    {
        AddPointEvent addPointInfo = new AddPointEvent();
        addPointInfo.eventDescription = "Losing points!";
        addPointInfo.point = - Mathf.Clamp( Random.Range(1, 10) , 0, PlayerPrefs.GetFloat("Highscore", 0));
        EventSystem.Current.FireEvent(addPointInfo);
    }
    public static bool Death()
    {
        return died;
    }
    public static void SetDied(bool d)
    {
        died = d;
    }
}
