//Author: Paschalis Tolios 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Callback
{

    public class SoundListener : MonoBehaviour
    {
        public GameObject SoundPrefab;
        private bool cooldown = false;
        private readonly float timeToWait = 0.4f;
        private GameObject go;
        private AudioSource audioSource;

        //Register Listener
        void Start()
        {
            EventSystem.Current.RegisterListener<SoundEvent>(PlaySound);
            EventSystem.Current.RegisterListener<StopSoundEvent>(StopSound);

        }

        //Stop sound that is being looped
        void StopSound(StopSoundEvent info)
        {
            Destroy(info.AudioPlayer);
        }

        //Instantiate an AudioPlayer
        void PlaySound(SoundEvent info)
        {
            if (!cooldown)
            {
                go = Instantiate(SoundPrefab);
                audioSource = go.GetComponent<AudioSource>();
                audioSource.clip = info.audioClip;
                audioSource.Play();


                if(info.volume != 0)
                {
                    audioSource.volume = info.volume;
                }

                if (info.parent != null)
                {
                    go.transform.SetParent(info.parent.transform);
                    go.transform.position = info.parent.transform.position;
                }
                else
                {
                    info.parent = Camera.main.gameObject;
                    go.transform.SetParent(info.parent.transform);
                    go.transform.position = info.parent.transform.position;
                }
                info.objectInstatiated = go;
                if (!info.looped)
                {
                    Destroy(go, audioSource.clip.length);
                }
             //   StartCoroutine(Cooldown());
            }
        }

        //Cooldown so that not too many sound being instantiated the same time
        IEnumerator Cooldown()
        {
            cooldown = true;
            yield return new WaitForSeconds(timeToWait);
            cooldown = false;
        }

        //Unregister Listener
        //private void OnDisable()
        //{
        //    if (EventSystem.Current != null)
        //    {
        //        EventSystem.Current.UnregisterListener<SoundEvent>(PlaySound);
        //        EventSystem.Current.UnregisterListener<StopSoundEvent>(StopSound);
        //    }
        //}

    }
}
