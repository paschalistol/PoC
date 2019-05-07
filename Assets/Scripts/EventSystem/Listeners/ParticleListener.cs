using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleListener : MonoBehaviour
{
    void Start()
    {
        EventSystem.Current.RegisterListener<SwitchLiftEvent>(OnLiftParticles);
        EventSystem.Current.RegisterListener<OpenDoorEvent>(OpenDoorParticles);
        EventSystem.Current.RegisterListener<FuseBoxEvent>(FuseBoxParticles);
        //EventSystem.Current.RegisterListener<OpenDoorEvent>(OpenDoorSound);
        //EventSystem.Current.RegisterListener<FuseBoxEvent>(FuseBoxSound);
    }


    void OnLiftParticles(SwitchLiftEvent info)
    {
        GameObject go = Instantiate(info.particles);
        ParticleSystem system = go.GetComponent<ParticleSystem>();
        go.transform.position = info.speaker.transform.position;
        system.Play();
        StartCoroutine(ParticleDelay(system, go));
        system.Pause();
    }

    void OpenDoorParticles(OpenDoorEvent info)
    {

        Debug.Log(info.gameObject );
        Debug.Log(info.particles);
        GameObject go = Instantiate(info.particles);
        Debug.Log(go.transform.position);

        ParticleSystem system = go.GetComponent<ParticleSystem>();
        go.transform.position = info.gameObject.transform.position;
        system.Play();
        StartCoroutine(ParticleDelay(system, go));
        system.Pause();
        Debug.Log(go.transform.position);
        Debug.Log("Running enumerator");  
    }

    void FuseBoxParticles(FuseBoxEvent info)
    {
        GameObject go = Instantiate(info.particles);
        ParticleSystem system = go.GetComponent<ParticleSystem>();
        go.transform.position = info.gameObject.transform.position;
        system.Play();
        StartCoroutine(ParticleDelay(system, go));
        
        Debug.Log("DoorOpenedListener2");
    }

    void WaterSplashParticles(WaterSplashEvent info)
    {
        GameObject go = Instantiate(info.particles);
        ParticleSystem system = go.GetComponent<ParticleSystem>();
        go.transform.position = info.gameObject.transform.position;
        system.Play();
        StartCoroutine(ParticleDelay(system, go));

        Debug.Log("waterSplash");
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
