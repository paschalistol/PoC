//Emil Dahl
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class DebugListener : MonoBehaviour
    {
        void Start()
        {
        EventSystem.Current.RegisterListener<SwitchEvent>(OnSwitch);
        }
        void OnSwitch(SwitchEvent info)
        {
            Debug.Log(info.eventDescription + info.switchedObject + " PriorityLevel: " + (info.VerbosityLvl = 1));
        }
    }

