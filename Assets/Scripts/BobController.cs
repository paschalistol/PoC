

using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class BobController : MonoBehaviour
{

    public Animator anim;
    public float speed;
    public float direction;
    private object rb;

    int jumpHash = Animator.StringToHash("Jump");
    int runStateHash = Animator.StringToHash("Base Layer.Run");

    void Start()
    {
        anim = GetComponent<Animator>();

    }

    void Update()
    {
        speed = Input.GetAxis("Vertical");
        direction = Input.GetAxis("Horizontal");

        anim.SetFloat("speed", speed);
        anim.SetFloat("direction", direction);

        if (Input.GetButtonDown("Fire1"))
            anim.SetTrigger("taunt");

        if (Input.GetKeyDown(KeyCode.Space))
        {

            anim.SetTrigger("jump");

            AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
            if (Input.GetKeyDown(KeyCode.Space) && stateInfo.nameHash == runStateHash)
            {
                anim.SetTrigger(jumpHash);
            }

        }
}
}
