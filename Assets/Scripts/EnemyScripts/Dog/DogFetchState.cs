using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/OldboiFetchState")]
public class DogFetchState : DogBaseState
{

    private float chaseDistance;
    private float hearingRange;
    [SerializeField] private float bustedDistance;

    public override void EnterState()
    {
        base.EnterState();
            Debug.Log("r");
    }
    public override void ToDo()
    {
        

    }
}
