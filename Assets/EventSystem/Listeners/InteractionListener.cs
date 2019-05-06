//Emil Dahl
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionListener : MonoBehaviour
{
    public GameObject lift;
    void Start()
    {


        EventSystem.Current.RegisterListener<SwitchLiftEvent>(OnSwitch);
  
    }
    // Switch between turning on or off the switch (this code will be the bases for
    // most interactions with electrical power withing the game) 
    void OnSwitch(SwitchLiftEvent info)
    {
            lift.GetComponent<Lift2>().onOff = !lift.GetComponent<Lift2>().onOff;
    }
}

