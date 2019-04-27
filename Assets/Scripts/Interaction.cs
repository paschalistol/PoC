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
                gameObject.GetComponent<Door>().InteractWithDoor();
                break;
            case 16:
                gameObject.GetComponent<>
            default:
                break;
        }
    }
}
