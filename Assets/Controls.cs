using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DisableControls());   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator DisableControls()
    {
        yield return new WaitForSeconds(30);
        gameObject.SetActive(false);
    }
}
