using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRespawnListener : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        EventSystem.Current.RegisterListener<RespawnEvent>(Respawn);
        //DeathEvent?
    }

    // Update is called once per frame
    void Respawn(RespawnEvent eventInfo)
    {
     //   eventInfo.gameObject.GetComponent<RespawnItem>().Respawn();
        Debug.Log("FiredEvent");
    }
}
