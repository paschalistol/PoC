//Main Author: Emil Dahl

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleListener : MonoBehaviour
{
    [SerializeField] private GameObject ParticlesPrefab;
    [SerializeField] private GameObject go;

    void Start()
    {
        EventSystem.Current.RegisterListener<ParticleEvent>(RunParticles);
        EventSystem.Current.RegisterListener<StopParticleEvent>(StopParticles);

        EventSystem.Current.RegisterListener<SwitchLiftEvent>(OnLiftParticles);
        EventSystem.Current.RegisterListener<OpenDoorEvent>(OpenDoorParticles);
        EventSystem.Current.RegisterListener<FuseBoxEvent>(FuseBoxParticles);
        //EventSystem.Current.RegisterListener<OpenDoorEvent>(OpenDoorSound);
        //EventSystem.Current.RegisterListener<FuseBoxEvent>(FuseBoxSound);
    }

    void RunParticles(ParticleEvent eventInfo)
    {
        go = Instantiate(eventInfo.particles);
        ParticleSystem system = go.GetComponent<ParticleSystem>();
        go.transform.position = eventInfo.objectPlaying.transform.position;
        system.Play();
        StartCoroutine(ParticleDelay(system, go));
        system.Pause();
    }

    void StopParticles(StopParticleEvent eventInfo)
    {
        Destroy(go);
    }


    void OnLiftParticles(SwitchLiftEvent info)
    {
        GameObject go = Instantiate(ParticlesPrefab);
        ParticleSystem system = go.GetComponent<ParticleSystem>();
        go.transform.position = info.speaker.transform.position;
        system.Play();
        StartCoroutine(ParticleDelay(system, go));
        system.Pause();
    }

    void OpenDoorParticles(OpenDoorEvent info)
    {
        GameObject go = Instantiate(info.particles);
        ParticleSystem system = go.GetComponent<ParticleSystem>();
        go.transform.position = info.gameObject.transform.position;
        system.Play();
        StartCoroutine(ParticleDelay(system, go));
        system.Pause();
    }

    void FuseBoxParticles(FuseBoxEvent info)
    {
        GameObject go = Instantiate(info.particles);
        ParticleSystem system = go.GetComponent<ParticleSystem>();
        go.transform.position = info.gameObject.transform.position;
        system.Play();
        StartCoroutine(ParticleDelay(system, go));
    }

    void WaterSplashParticles(WaterSplashEvent info)
    {
        GameObject go = Instantiate(info.particles);
        ParticleSystem system = go.GetComponent<ParticleSystem>();
        go.transform.position = info.gameObject.transform.position;
        system.Play();
        StartCoroutine(ParticleDelay(system, go));
    }


    private IEnumerator ParticleDelay(ParticleSystem system, GameObject ob)
    {
        while (system.isEmitting || system.particleCount > 0)
        {
            yield return null;
        }
       Destroy(ob);
    }
}
