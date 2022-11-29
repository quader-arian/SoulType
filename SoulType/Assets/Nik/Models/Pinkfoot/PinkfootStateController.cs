using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinkfootStateController : MonoBehaviour
{
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        bool isWalking = animator.GetBool("isWalking");
        bool press1 = Input.GetKey("1");
        bool press2 = Input.GetKey("2");
        bool press3 = Input.GetKey("3");
        bool press4 = Input.GetKey("4");

        // if not walking and press 1
        if (!isWalking && press1)
        {
            // start walking
            animator.SetBool("isWalking", true);
        }

        // if walking and not pressing 1
        if (isWalking && !press1)
        {
            // stop walking
            animator.SetBool("isWalking", false);
        }

        if (press2)
        {
            animator.SetBool("isPreparing", true);
        }

        if (!press2)
        {
            animator.SetBool("isPreparing", false);
        }

        if (press3)
        {
            animator.SetBool("isAttacking", true);
        }

        if (!press3)
        {
            animator.SetBool("isAttacking", false);
        }

        if (press4)
        {
            animator.SetBool("isFlinching", true);
        }

        if (!press4)
        {
            animator.SetBool("isFlinching", false);
        }
    }
}
