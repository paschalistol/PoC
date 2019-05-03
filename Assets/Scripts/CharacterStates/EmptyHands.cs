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
            SetHolding(true);
            
        }
        if (HoldingSth)
        {
            owner.ChangeState<HoldingItem>();
        }
    }
}
