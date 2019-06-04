using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Callback
{
    public class PressEListener : MonoBehaviour
    {
        [SerializeField] private GameObject pressEGameObject;
        [SerializeField] private TMPro.TMP_Text warningText;
        private GameObject warningTextObject;
        private readonly float timeToWait = 1f;

        void Start()
        {
            EventSystem.Current.RegisterListener<PressE>(ToggleText);
            EventSystem.Current.RegisterListener<WarningEvent>(ToggleText);
            warningTextObject = warningText.gameObject.transform.parent.gameObject;
        }

        void ToggleText(PressE info)
        {
            pressEGameObject.SetActive(info.open);
        }
        void ToggleText(WarningEvent info)
        {
            warningTextObject.SetActive(true);
            warningText.text = info.warning;
            StartCoroutine(CloseCountDown());

        }
        IEnumerator CloseCountDown()
        {
            yield return new WaitForSeconds(timeToWait);
            warningTextObject.SetActive(false);
        }
    }
}