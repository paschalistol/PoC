using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PhysicsCube : MonoBehaviour
{
    protected Vector3 velocity;
    protected BoxCollider boxCollider;
    private PhysicsScript whereAreMyDragons;

    // Start is called before the first frame update
    void Start()
    {
        whereAreMyDragons = GetComponent<PhysicsScript>();
        boxCollider = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Running cube update!");
        velocity = whereAreMyDragons.Gravity(velocity);
        velocity = whereAreMyDragons.BoxCollisionCheck(velocity, boxCollider);
        velocity = whereAreMyDragons.Decelerate(velocity);
        //velocity = whereAreMyDragons.Decelerate(whereAreMyDragons.BoxCollisionCheck(velocity = whereAreMyDragons.Gravity(velocity), boxCollider));
        transform.position += velocity * Time.deltaTime;
    }
}
