//Main Author: Emil Dahl

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static bool isPaused = false;
    public static bool activatedAlarm = false;
    public static bool disabledAlaram = false;
    public static bool VictortCondition = false;
    public GameObject tint;

    [SerializeField] private GameObject[] lightHolders;
    [SerializeField] private AudioClip alarmClip;
    [SerializeField] private float tintTime = 0.5f;
    [SerializeField] private GameObject player;
    private SoundEvent soundEvent;
    private StopSoundEvent stopSound;
    [SerializeField] private float volume; 
    
    private bool tintCondtion = true;
    private float currentTime;
    private bool usedOnce;

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
        if (activatedAlarm == true)
        {
            BlinkingTint();
            ActivateLights();

            if (!usedOnce)
            {
                soundEvent = new SoundEvent();
                soundEvent.audioClip = alarmClip;
                soundEvent.looped = true;
                soundEvent.parent = player;
                soundEvent.volume = volume;

                EventSystem.Current.FireEvent(soundEvent);
                usedOnce = true;
            }
        }else if(soundEvent != null)
        {
            stopSound = new StopSoundEvent();
            stopSound.AudioPlayer = soundEvent.objectInstatiated;

            if(stopSound.AudioPlayer != null)
            EventSystem.Current.FireEvent(stopSound);
        }

    }


    void AlarmReset(UnitDeathEventInfo info)
    {
        activatedAlarm = false;
        tint.SetActive(false);
    }

    void ActivateLights()
    {
        foreach (GameObject ob in lightHolders)
            if (ob != null)
            {
                ob.GetComponent<Light>().intensity = 100;
                ob.GetComponent<Light>().color = Color.red;
            }
       
    }

    void BlinkingTint()
    {
        if (activatedAlarm)
        {
            if (currentTime <= 0)
            {
                tintCondtion = !tintCondtion;
                tint.SetActive(tintCondtion);
                currentTime = tintTime;
            }
            currentTime -= Time.deltaTime;
        }
    }
}
