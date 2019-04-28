//Emil Dahl
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EventInfo
{
    public string eventDescription;
}

public class DebugEvent : EventInfo
{
    public int VerbosityLvl;
}
public class UnitDeathEventInfo : DebugEvent
{
    public GameObject deadUnit;
    //public AudioClip audioClip;
    //public GameObject particleSystem;
}

public class SwitchEvent : DebugEvent
{
    public GameObject switchedObject;
    public GameObject speaker;
    public AudioClip audioClip;
    public GameObject particles;
    public int timesCalled;
}
    public class InteractionEvent : DebugEvent
{
    public GameObject interactedObject;
}

    public class UnlockEvent : DebugEvent
    {
        public GameObject doorObject;
    }
