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
    public TMPro.TMP_Text deathcounterText;
    public SaveSystem saveSystem;
    private static bool died = false;
    [SerializeField] private float minPointsToLose = 1, maxPointsToLose = 10;
    void Start()
    {
        EventSystem.Current.RegisterListener<UnitDeathEventInfo>(DeathInteraction);
        deathcounterText.text = "" + PlayerPrefs.GetInt("DeathCounter");
        saveSystem = GameObject.Find("SaveManager").GetComponent<SaveSystem>();
    }

    void DeathInteraction(UnitDeathEventInfo deathInfo)
    {

        objectToInteract = deathInfo.deadUnit;
        PlayerPrefs.SetInt("DeathCounter", PlayerPrefs.GetInt("DeathCounter") +1);
        deathcounterText.text = ""+PlayerPrefs.GetInt("DeathCounter");
        DecreaseHighscore();
        died = true;
        saveSystem.died = true;
        saveSystem.Load();
        
    }
    public static bool Death()
    {
        return died;
    }
    public static void SetDied(bool d)
    {
        died = d;
    }
    void DecreaseHighscore()
    {
        AddPointEvent addPointInfo = new AddPointEvent();
        addPointInfo.eventDescription = "Losing points!";
        addPointInfo.point = - Mathf.Clamp( Random.Range(minPointsToLose, maxPointsToLose) , 0, PlayerPrefs.GetFloat("Highscore", 0));
        EventSystem.Current.FireEvent(addPointInfo);

    }
}
