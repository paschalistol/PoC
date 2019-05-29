using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static bool isPaused = false;
    public static bool activatedAlarm = false;
    public static bool disabledAlaram = false;

    private void Start()
    {
        EventSystem.Current.RegisterListener<UnitDeathEventInfo>(AlarmControl);
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

    void AlarmControl(UnitDeathEventInfo info)
    {
        Debug.Log("EventFired");
        activatedAlarm = false;
    }
}
