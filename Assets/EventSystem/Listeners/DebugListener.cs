//Emil Dahl
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class DebugListener : MonoBehaviour
    {
        void Start()
        {
        EventSystem.Current.RegisterListener<SwitchLiftEvent>(OnSwitch);
        }
        void OnSwitch(SwitchLiftEvent info)
        {
            Debug.Log(info.eventDescription + info.switchedObject + " PriorityLevel: " + (info.VerbosityLvl = 1));
        }
    }

