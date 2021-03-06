﻿//Main Author: Emil Dahl

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleListener : MonoBehaviour
{
    private GameObject ParticlesPrefab;
    private ParticleSystem system;
    private GameObject go;

    void Start()
    {
        EventSystem.Current.RegisterListener<ParticleEvent>(RunParticles);
        EventSystem.Current.RegisterListener<StopParticleEvent>(StopParticles);
    }

    void RunParticles(ParticleEvent eventInfo)
    {
        go = Instantiate(eventInfo.particles);
        system = go.GetComponent<ParticleSystem>();
        go.transform.position = eventInfo.objectPlaying.transform.position;
        eventInfo.particles = go;
        system.Play();

        if (!system.isEmitting && go != null)
        {
            Destroy(go);
        }

    }

    void StopParticles(StopParticleEvent eventInfo)
    {
        if(system != null)
        Destroy(eventInfo.particlesToStop);
    }


}

#region ParticleLegacy

    //void CustomTimer(GameObject go)
    //{
    //    currentTime = startTime;

    //    while (currentTime >= 0)
    //    {
    //        currentTime -= Time.deltaTime;
    //    }
    //    Destroy(go);

    //}
//Legacy
    //private IEnumerator ParticleDelay(ParticleSystem system)
    //{
    //    while (system.isEmitting || system.particleCount > 0)
    //    {
            
    //        yield return null;
    //    }

    //    if (go != null)
    //    {
    //        Destroy(go);
    //    }
    //}

//private GameObject go;
//EventSystem.Current.RegisterListener<SwitchLiftEvent>(OnLiftParticles);
//EventSystem.Current.RegisterListener<OpenDoorEvent>(OpenDoorParticles);
//EventSystem.Current.RegisterListener<FuseBoxEvent>(FuseBoxParticles);
//EventSystem.Current.RegisterListener<StopParticleEvent>(StopParticles);
//EventSystem.Current.RegisterListener<OpenDoorEvent>(OpenDoorSound);
//EventSystem.Current.RegisterListener<FuseBoxEvent>(FuseBoxSound);
//void StopParticles(StopParticleEvent eventInfo)
//{
//    Destroy(go);
//}

//void OnLiftParticles(SwitchLiftEvent info)
//{
//    GameObject go = Instantiate(ParticlesPrefab);
//    ParticleSystem system = go.GetComponent<ParticleSystem>();
//    go.transform.position = info.speaker.transform.position;
//    system.Play();
//    StartCoroutine(ParticleDelay(system, go));
//    system.Pause();
//}

//void OpenDoorParticles(OpenDoorEvent info)
//{
//    GameObject go = Instantiate(info.particles);
//    ParticleSystem system = go.GetComponent<ParticleSystem>();
//    go.transform.position = info.gameObject.transform.position;
//    system.Play();
//    StartCoroutine(ParticleDelay(system, go));
//    system.Pause();
//}

//void FuseBoxParticles(FuseBoxEvent info)
//{
//    GameObject go = Instantiate(info.particles);
//    ParticleSystem system = go.GetComponent<ParticleSystem>();
//    go.transform.position = info.gameObject.transform.position;
//    system.Play();
//    StartCoroutine(ParticleDelay(system, go));
//}

//void WaterSplashParticles(WaterSplashEvent info)
//{
//    GameObject go = Instantiate(info.particles);
//    ParticleSystem system = go.GetComponent<ParticleSystem>();
//    go.transform.position = info.gameObject.transform.position;
//    system.Play();
//    StartCoroutine(ParticleDelay(system, go));
//}
#endregion