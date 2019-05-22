//Author: Paschalis Tolios

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Interactable
{
    [SerializeField]
    private int rotationSpeed = 40, rotation = 90;
    private GameObject parent;
    private bool used = false;
    [Header("Sounds")]
    [SerializeField] private AudioClip doorOpenSound;
    [SerializeField] private AudioClip doorCloseSound;
    [SerializeField] private AudioClip unlockDoorSound;
    private float rotationGoal;
    protected override void Start()
    {
        base.Start();
    }
    public override AudioClip GetAudioClip()
    {
        return doorOpenSound;
    }

    private void Awake()
    {
        parent = transform.parent.gameObject;
    }

    public override void StartInteraction()
    {
        if (used == false)
        {

            StartCoroutine(RotateDoor(parent));
            used = true;
        }
        if (gameObject.GetComponent<ActiveGold>() != null)
        {
            gameObject.GetComponent<ActiveGold>().SetGoldActive();
        }
    }
    int temp;
    IEnumerator RotateDoor(GameObject parent)
    {
        rotationGoal = parent.transform.eulerAngles.y + rotation;
        temp = (int)(rotationGoal / 360);

        rotationGoal = rotationGoal % 360;
        parent.transform.eulerAngles = new Vector3(0, parent.transform.eulerAngles.y- 360 * temp, 0);
        while ((parent.transform.eulerAngles.y) % 360 < rotationGoal)
        {

            parent.transform.eulerAngles += new Vector3(0, 1, 0) * Time.deltaTime * rotationSpeed;

            yield return null;
        }
    }
}
