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
        public  float cooldownTime = 0.4f;

        void Start()
        {
            EventSystem.Current.RegisterListener<SoundEvent>(PlaySound);

        }
        void PlaySound(SoundEvent info)
        {
            if (!cooldown)
            {
                GameObject go = Instantiate(SoundPrefab);
                AudioSource audioSource = go.GetComponent<AudioSource>();
                audioSource.clip = info.audioClip;
                audioSource.Play();
                Destroy(go, audioSource.clip.length);
                StartCoroutine(Cooldown());
            }
        }
        IEnumerator Cooldown()
        {
            cooldown = true;
            yield return new WaitForSeconds(cooldownTime);
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
