using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WraithStateController : MonoBehaviour
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
        bool press2 = Input.GetKey("2");
        bool press3 = Input.GetKey("3");
        bool press4 = Input.GetKey("4");

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
