//Author Johan Ekman
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinListener : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        EventSystem.Current.RegisterListener<WinningEvent>(Winning);
    }

    // Update is called once per frame
    void Winning(WinningEvent winEvent)
    {
        Debug.Log("you won! uwu");
    }
}
