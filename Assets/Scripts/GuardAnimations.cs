using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardAnimations : MonoBehaviour
{
    private Animator anim;    

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();        
    }

    // Update is called once per frame
    void Update()
    {
        anim.Play("pickup", 1, 60f);
        anim.Update(0f);
    }
}
