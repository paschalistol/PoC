using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{

    public void StartInteraction(GameObject gameObject)
    {


        switch (gameObject.tag)
        {
            //case 11:


            //    gameObject.GetComponent<Boxes2>().InteractWithBox();

            //break;
            //case 15:
            ////    //bool hasKey = gameObject.GetComponent<Key>().used;
            ////gameObject.GetComponent<Door>().InteractWithDoor();

            //break;
            //case "Battery":
            //   gameObject.GetComponent<Battery>().PickUpBattery();
            //  break;
            //case 17:
            //    //gameObject.GetComponent<Key>().TakeKeyInteraction();
            //    break;
            case "SceneTransfer":
                gameObject.GetComponent<SceneTransfer>().ChangeLevel();
                break;
            default:
                break;
        }
    }
}
