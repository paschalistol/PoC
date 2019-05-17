//Author: Paschalis Tolios
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{

    private float rotationX, rotationY;
    [SerializeField] private float mouseSensitivity = 1;
    [SerializeField] private float minDown = -12;
    [SerializeField] private float maxUp = 65;
    public GameObject character;
    [SerializeField] private Vector3 distanceToChar = new Vector3(0.13f, 4, -20);
    private const float radius = 0.05f;
    public LayerMask layerMask;
    private bool FirstPers;
    private Vector3 relationshipToChar;

    private void Awake()
    {
        rotationY = transform.eulerAngles.y;
    }

    void Update()
    {
        GetMouseInput();
        relationshipToChar = transform.rotation * distanceToChar;
        if (!FirstPers)
        {
            transform.position = CollisionCheck() + character.transform.position;
        }
        else
        {
            transform.position = character.transform.position;
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            FirstPers = !FirstPers;
        }


    }
    void GetMouseInput()
    {
        rotationX -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        rotationY += Input.GetAxis("Mouse X") * mouseSensitivity;
        rotationX = Mathf.Clamp(rotationX, minDown, maxUp);
        transform.rotation = Quaternion.Euler(rotationX, rotationY, 0);

    }
    Vector3 CollisionCheck()
    {
        RaycastHit raycastHit;
        Vector3 move = new Vector3(0, 0, 0);
        bool sphereCast = Physics.SphereCast(character.transform.position, radius, relationshipToChar, out raycastHit, (relationshipToChar ).magnitude, layerMask);
        Debug.DrawRay(character.transform.position, relationshipToChar, Color.red);
        if (raycastHit.collider != null)
        {
            move = relationshipToChar - relationshipToChar.normalized * raycastHit.distance;
        }
        return relationshipToChar - move;
    }
    public Vector3 getRelationship()
    {
        return relationshipToChar;
    }
}

