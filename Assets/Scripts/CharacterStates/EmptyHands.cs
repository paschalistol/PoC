//Author: Paschalis Tolios

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Character/EmptyHands")]
public class EmptyHands : HoldItemBase
{
    public override void EnterState()
    {
        base.EnterState();

    }
    public override void ToDo()
    {

        if (Input.GetKeyDown(KeyCode.E) && ReturnObjectInFront() != null)
        {


            objectCarried = ReturnObjectInFront();
            InteractWithObject();
            if (!objectCarried.CompareTag("Only Interaction"))
            {
                SetHolding(true);
            }

        }
        if (DeathListener.Death()) 
        {
            DeathListener.SetDied(false);
        }
        if (HoldingSth)
        {
            owner.ChangeState<HoldingItem>();
        }
    }
}
