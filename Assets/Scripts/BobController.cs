using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BobController : MonoBehaviour
{

    public Animator anim;
    public float speed;
    public float direction;    
    public GameObject player;    

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        speed = Input.GetAxis("Vertical");
        direction = Input.GetAxis("Horizontal");
        anim.SetFloat("Direction", direction);

        if (!Input.GetKey(KeyCode.LeftShift))
        {
            anim.SetFloat("Speed", speed);            
        }
        else if (Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.S))
        {
            anim.SetFloat("Speed", 2f);
        }
        else if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.S))
        {
            anim.SetFloat("Speed", -2f);
        }
              

        if (Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetTrigger("Jump");            
        }

        if(player.GetComponent<CharacterStateMachine>().grounded == true)
        {
            anim.SetBool("Grounded", true);
        }
        else
        {
            anim.SetBool("Grounded", false);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            anim.SetTrigger("Pickup");
        }             
    }
}
