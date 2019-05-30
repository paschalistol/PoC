//Main Author: Emil Dahl

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static bool isPaused = false;
    public static bool activatedAlarm = false;
    public static bool disabledAlaram = false;

    private GameObject[] lightHolders;

    

    private void Start()
    {
        EventSystem.Current.RegisterListener<UnitDeathEventInfo>(AlarmReset);

        
        foreach (GameObject ob in lightHolders)
            if(ob != null)
            ob.GetComponent<Light>().intensity = 0;
    }
    
    void Update()
    {
        PauseController();
    }

    void PauseController()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            isPaused = !isPaused;
    }

    void AlarmReset(UnitDeathEventInfo info)
    {
        Debug.Log("EventFired");
        activatedAlarm = false;
    }

    //void AlarmControl(RunningAlarm alarm)
    //{

    //}

    void ActivateLights()
    {
        foreach (GameObject ob in lightHolders)
            if (ob != null)
                ob.GetComponent<Light>().intensity = 100;
    }
}
