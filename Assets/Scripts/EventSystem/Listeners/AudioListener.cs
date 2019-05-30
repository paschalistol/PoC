

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AudioListener : MonoBehaviour
{
    public GameObject lift;

    void Start()
    {
        EventSystem.Current.RegisterListener<SwitchLiftEvent>(OnLiftSound);
        //EventSystem.Current.RegisterListener<OpenDoorEvent>(OpenDoorSound);
        //EventSystem.Current.RegisterListener<FuseBoxEvent>(FuseBoxSound);
    }

    
    void OnLiftSound(SwitchLiftEvent info)
    {
        AudioSource source = GetComponent<AudioSource>();
        source.clip = info.audioClip;
        source.Play();
        StartCoroutine(Delay(source, info));

        //GameObject go = Instantiate(info.speaker);
        //AudioSource system = go.GetComponent<AudioSource>();
        //go.transform.position = info.speaker.transform.position;
        //system.Play();
        //StartCoroutine(Delay(go, system, info));
    }
    private IEnumerator Delay(AudioSource system, SwitchLiftEvent info2)
    {
        //this timer just makes sure we don't hit pause instantly before the sound can start playing
        float startTime = 2
            ;
        float currTime = startTime;

        while (currTime > 0 || lift.GetComponent<Lift2>().onOff == true)
        {
            currTime--;
            Debug.Log(lift.GetComponent<Lift2>().onOff);
            yield return null;
        }
        system.Pause();
        //Debug.Log("Exists: " + ob);
        //  Destroy(ob);
    }
}
