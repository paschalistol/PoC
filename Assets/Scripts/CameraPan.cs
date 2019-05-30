using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPan : MonoBehaviour
{





    void Update()
    {
        transform.Rotate(new Vector3(0f, 0.5f, 0f));
        
    }
}
