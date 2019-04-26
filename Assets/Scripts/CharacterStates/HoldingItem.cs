using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Character/HoldingItem")]
public class HoldingItem : HoldItemBase
{

    public override void EnterState()
    {

        base.EnterState();
        objectCarried = ReturnObjectInFront();

        if (objectCarried != null)
        {

        objectCarried.layer = 0;
        }
    }
    public override void ToDo()
    {

        if (Input.GetKeyDown(KeyCode.E))
        {
            objectCarried.layer = 11;
            SetHolding(false);
        }
        // if (Input.GetKeyDown(KeyCode.R))
        //{
           
        //    objectCarried.layer = 11;
        //    objectCarried.transform.position += ThrowTo();

        //    objectCarried.GetComponent<Rigidbody>().AddForce(ThrowTo()*10);
        //   // objectCarried.GetComponent<Boxes>().ApplyForce(ThrowTo());

        //    SetHolding(false);

        //}
        if (!HoldingSth)
        {
            owner.ChangeState<EmptyHands>();
        }
        if (objectCarried != null)
        {
            TransformCarriedObject();
        }
    }

    private void TransformCarriedObject()
    {
        Vector3 target;
        target.x = 0;
        target.y = Camera.main.transform.eulerAngles.y;
        target.z = 0;
        objectCarried.transform.eulerAngles = target;
        objectCarried.transform.position = Direction();
    }

    Vector3 ThrowTo()
    {

        float x = LookDirection().x * 3 ;
        float y = 50;
        float z = LookDirection().z * 3 ;
        return new Vector3(x, y, z);

    }
    private Vector3 Direction()
    {
        Vector3 project = Vector3.ProjectOnPlane(LookDirection(), Vector3.down).normalized;
        float x = owner.transform.position.x + project.x * (capsuleCollider.radius + objectCarried.GetComponent<BoxCollider>().transform.localScale.x / 2);
        float y = objectCarried.transform.localScale.y/2 + capsuleCollider.height/2 + owner.transform.position.y;
        float z = owner.transform.position.z + project.z * (capsuleCollider.radius + objectCarried.GetComponent<BoxCollider>().transform.localScale.z / 2);
        return new Vector3(x, y, z);
    }


}