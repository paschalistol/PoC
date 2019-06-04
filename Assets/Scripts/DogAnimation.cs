using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogAnimation : MonoBehaviour
{
    private Animator anim;
    public GameObject dog;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.Play("C4D Animation Take", 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (dog.GetComponent<EnemyDog>().isInChase)
        {            
            anim.SetBool("chase", true);
        }
        else
        {
            anim.SetBool("chase", false);
        }
    }
}
