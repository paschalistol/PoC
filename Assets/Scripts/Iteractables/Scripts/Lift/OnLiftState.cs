using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Lift/OnLiftState")]
public class OnLiftState : GroundedState
{

    private LayerMask mask;
    

    public void Awake()
    {
 
    }

    public override void EnterState()
    {
        base.EnterState();
        dynamicFriction = 0.35f;
        MaxSpeed = 10;
        
        
    }

    public override void ToDo()
    {
        base.ToDo();
        CollisionCheck();
        
        Debug.Log("CollisionOnCharacter!");
       
        /**
         * Lägger på force som gör att den går in i jump
         * 
         * aaaaa
         * **/
        ApplyForce(new Vector3(0, owner.transform.position.y - owner.lift2.transform.position.y, 0));
        owner.transform.parent = owner.lift2.transform;
    }

}
