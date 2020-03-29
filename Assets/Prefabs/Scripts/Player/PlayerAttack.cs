using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    Animator animator;
    Transform body;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = this.gameObject.transform.GetChild(0).position;


        if (Input.GetButtonDown("Fire1") == true)
        {
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("SwordSwing"))
            {
                animator.SetBool("isAttacking", true);
            }
        }
        else
        {
            animator.SetBool("isAttacking", false);
        }
    }
}
