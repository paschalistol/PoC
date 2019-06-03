//Author: Paschalis Tolios

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Character/EmptyHands")]
public class EmptyHands : HoldItemBase
{

    PressE pressE;
    GameObject objectInFront;

    public override void EnterState()
    {
        base.EnterState();
        pressE = new PressE();

    }



    public override void ToDo()
    {

        objectInFront = ReturnObjectInFront();

        if (objectInFront != null && objectInFront.CompareTag("Valuables") == false)
        {
            pressE.open = true;
            EventSystem.Current.FireEvent(pressE);

            if (Input.GetKeyDown(KeyCode.E))
            {

                owner.objectCarried = objectInFront;
                InteractWithObject();
                
                if (!owner.objectCarried.CompareTag("Only Interaction"))
                {
                    SetHolding(true);
                }

            }
        }
        else
        {
            pressE.open = false;
            EventSystem.Current.FireEvent(pressE);
        }

        if (HoldingSth)
        {
            owner.ChangeState<HoldingItem>();
        }
        
    }


    public override void ExitState()
    {
        pressE.open = false;
        EventSystem.Current.FireEvent(pressE);
    }
}
