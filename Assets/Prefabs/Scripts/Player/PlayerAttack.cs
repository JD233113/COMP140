using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    Animator animator;
    Transform body;
    Rigidbody2D rb;

    public ControllerInput controllerInput;
    public GameObject blockSprite;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        rb = this.gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = this.gameObject.transform.GetChild(0).position;

        //Attacks upon recieving associated keyboard/controller input
        if (Input.GetButtonDown("Fire1") == true || controllerInput.state == 1)
        {
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("SwordSwing"))
            {
                animator.SetBool("isAttacking", true);
                blockSprite.gameObject.SetActive(false);
            }
        }
        else
        {
            animator.SetBool("isAttacking", false);
        }

        //Blocks upon recieving associated controller input
        if (controllerInput.state == 6)
        {
            blockSprite.gameObject.SetActive(true);
        }
    }
}
