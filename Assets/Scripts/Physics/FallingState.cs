using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Physics/FallingState")]
public class FallingState : PhysicsBaseState
{
    public override void EnterState()
    {
        base.EnterState();
        dynamicFriction = 0.35f;
    }

    public override void ToDo()
    {
        base.ToDo();
        Gravity();
        CollisionCheck();
        owner.transform.position += Velocity * Time.deltaTime;
        Debug.Log("Running this mother f*ing code!");
    }



}