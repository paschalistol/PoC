using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Callback
{
    public class PressEListener : MonoBehaviour
    {
        [SerializeField] private GameObject text;
        void Start()
        {
            EventSystem.Current.RegisterListener<PressE>(ToggleText);
        }

        void ToggleText(PressE info)
        { 
            text.SetActive(info.open);
        }
    }
}