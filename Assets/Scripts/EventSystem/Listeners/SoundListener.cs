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
        private SoundEvent se;
        void Start()
        {
            EventSystem.Current.RegisterListener<SoundEvent>(PlaySound);

        }

        public GameObject GetGO()
        {
            return go;
        }
        void PlaySound(SoundEvent info)
        {
            se = info;
            if (!cooldown)
            {
                go = Instantiate(SoundPrefab);
                audioSource = go.GetComponent<AudioSource>();
                audioSource.clip = info.audioClip;
                audioSource.Play();
                
                if (!info.looped)
                {

                    Destroy(go, audioSource.clip.length);
                }
                StartCoroutine(Cooldown());
            }
        }
        IEnumerator Cooldown()
        {
            cooldown = true;
            yield return new WaitForSeconds(timeToWait);
            cooldown = false;
        }
        private void OnDisable()
        {
            if (EventSystem.Current != null)
            {
                EventSystem.Current.UnregisterListener<SoundEvent>(PlaySound);
            }

        }

    }
}
