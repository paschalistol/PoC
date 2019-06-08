//Main Author: Emil Dahl

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// General GameController to take care of functionality that impacts many different targets
/// </summary>
public class GameController : MonoBehaviour
{
    [SerializeField] private AudioClip alarmClip;
    [SerializeField] private GameObject player;
    [SerializeField] private float tintTime = 0.5f;
    [SerializeField] private float volume; 

    public GameObject tint;
    private SoundEvent soundEvent;
    private StopSoundEvent stopSound;

    #region StaticBools
    public static bool isPaused = false;
    public static bool activatedAlarm = false;
    public static bool disabledAlaram = false;
    public static bool VictortCondition = false;
    #endregion

    private bool tintCondtion = true;
    private bool usedOnce = false;
    private float currentTime;

    private void Start()
    {
        EventSystem.Current.RegisterListener<UnitDeathEventInfo>(AlarmReset);
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

            if (!usedOnce)
            {
                soundEvent = new SoundEvent();
                soundEvent.audioClip = alarmClip;
                soundEvent.looped = true;
                soundEvent.parent = Camera.main.gameObject;
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

    /// <summary>
    /// If the player dies the alarm is reset
    /// </summary>
    /// <param name="info"></param>
    void AlarmReset(UnitDeathEventInfo info)
    {
        activatedAlarm = false;
        tint.SetActive(false);
    }

    /// <summary>
    /// Function for making the screen blink through the duration of the alarm
    /// </summary>
    public void BlinkingTint()
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
        else
        {
            tint.SetActive(false);
        }
    }
}
#region ControllerLegacy

    //void ActivateLights()
    //{
    //    foreach (GameObject ob in lightHolders)
    //        if (ob != null)
    //        {
    //            ob.GetComponent<Light>().intensity = 100;
    //            ob.GetComponent<Light>().color = Color.red;
    //        }
       
    //}

#endregion
