using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{

    public void StartInteraction(GameObject gameObject)
    {
        switch (gameObject.layer)
        {
            case 15:
            //    //bool hasKey = gameObject.GetComponent<Key>().used;
            gameObject.GetComponent<Door>().InteractWithDoor();

            break;
            case 16:
             //   gameObject.GetComponent<Battery>().PickUpBattery();
                break;
            case 17:
               // gameObject.GetComponent<Key>().TakeKeyInteraction();
                break;
            case 18: 

            default:
                break;
        }
    }
}
