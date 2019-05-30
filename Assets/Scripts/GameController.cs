//Main Author: Emil Dahl

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static bool isPaused = false;
    public static bool activatedAlarm = false;
    public static bool disabledAlaram = false;

    [SerializeField] private GameObject[] lightHolders;
    [SerializeField] private AudioClip clip;
    [SerializeField] private GameObject tint;
    private const float tintTime = 2f;
    private bool tintCondtion = true;
    private float currentTime;



    private void Start()
    {
        EventSystem.Current.RegisterListener<UnitDeathEventInfo>(AlarmReset);

        foreach (GameObject ob in lightHolders)
            if (ob != null)
                ob.GetComponent<Light>().intensity = 0;
    }

    void Update()
    {
        PauseController();
        AlarmController();
    }

    void PauseController()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            isPaused = !isPaused;
    }

    void AlarmController()
    {
        Debug.Log("ControllerActivation");
        if (activatedAlarm == true)
        {
            BlinkingTint();
            ActivateLights();

            SoundEvent soundEvent = new SoundEvent();
            soundEvent.audioClip = clip;
            soundEvent.looped = true;

            EventSystem.Current.FireEvent(soundEvent);

        }
    }

    void AlarmReset(UnitDeathEventInfo info)
    {
        activatedAlarm = false;
    }

    void ActivateLights()
    {
        foreach (GameObject ob in lightHolders)
            if (ob != null)
            {
                ob.GetComponent<Light>().intensity = 100;
                ob.GetComponent<Light>().color = Color.red;
            }
        Debug.Log("LightActivation");
    }

    void BlinkingTint()
    {
        currentTime = tintTime;

        tint.SetActive(tintCondtion);

        while (activatedAlarm)
        {  
            if (currentTime <= 0)
            {
                tint.SetActive(tintCondtion);
                tintCondtion = !tintCondtion;
            }
            currentTime = tintTime;

            currentTime -= Time.deltaTime;
            currentTime = tintTime;

        }


    }
}
