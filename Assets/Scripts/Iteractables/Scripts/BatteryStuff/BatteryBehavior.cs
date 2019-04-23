using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryBehavior : MonoBehaviour
{
    public GameObject battery;
    public GameObject fuseBox;

    public GameObject speaker;
    public AudioClip clip;
   // [SerializeField]private float plugDistance = 10f;
 
    /**
     * Battery always on player, change to battery object later. 
     * 
     * 
     * **/
    void Update()
    {
        //Debug.Log("position of char" + transform.position + " position of fusebox" + fuseBox.transform.position);
        if ((Vector3.Distance(transform.position, fuseBox.transform.position) < 3) && Input.GetKeyDown(KeyCode.E))
        {
            SwitchEvent switchedInfo = new SwitchEvent ();
            switchedInfo.eventDescription = "Pressed item has been activated: " ;
            switchedInfo.switchedObject = fuseBox.gameObject;
            switchedInfo.speaker = speaker;
            switchedInfo.audioClip = clip;
            


            EventSystem.Current.FireEvent(switchedInfo);
        }
    }
}
