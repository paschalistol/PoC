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
    public GameObject spawnPoint;
    //public AudioClip audioClip;
    //public GameObject particleSystem;
}

public class SwitchLiftEvent : DebugEvent
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
public class SoundEvent : DebugEvent
{
    public AudioClip audioClip;

    public GameObject gameObject;
}

public class FuseBoxEvent : DebugEvent
{
    public GameObject particles;
    public GameObject gameObject;
}

public class OpenDoorEvent : DebugEvent
{
    public AudioClip audioClip;
    public GameObject gameObject;
    public GameObject particles;
}

public class TookKeyEvent : DebugEvent
{
    public AudioClip audioClip;
    public GameObject gameObject;
}

public class WaterSplashEvent : DebugEvent
{
    public AudioClip audioClip;
    public GameObject particles;
    public GameObject gameObject;
}

public class ChaseEvent : DebugEvent
{
    public GameObject gameObject;
    public GameObject audioSpeaker;
}

public class WinningEvent : DebugEvent
{
    public GameObject gameObject;
}