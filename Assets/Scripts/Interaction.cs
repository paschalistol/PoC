//Author: Paschalis Tolios
//Secondary Authors: Emil Dahl, Johan Ekman
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{

    public void StartInteraction(GameObject gameObject)
    {
        switch (gameObject.tag)
        {

            case "SceneTransfer":
                gameObject.GetComponent<SceneTransfer>().ChangeLevel();
                break;
            case "Key":
                gameObject.GetComponent<Key>().KeyInteraction();
                break;
            case "FuseBoxItem":
                gameObject.GetComponent<FuseBoxItem>().isHeld = !gameObject.GetComponent<FuseBoxItem>().isHeld;
                break;
            case "Valuables":
                gameObject.GetComponent<Valuable>().AddPoint();
                break;
            case "Battery":
                gameObject.GetComponent<Battery>().isHeld = !gameObject.GetComponent<Battery>().isHeld;
                break;
            default:
                break;
        }
    }
}
