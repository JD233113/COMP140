using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    public float movementScale;
    public float jumpScale;
    public float maxSpeed;
    bool isGrounded;
    float scale;
    bool facingRight = true;

    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        //Stores the rigidbody to reduce the number of times GetComponent() needs to be called
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        scale = transform.localScale.x;
    }

    void FixedUpdate()
    {
        //Checks whether the movement inputs are being used
        float xMovement = Input.GetAxis("Horizontal");

        //Applies horizontal force to the rigidbody when the player attacks
        if (Input.GetButtonDown("Fire1") == true && rb.velocity.magnitude < maxSpeed)
        {
            if (facingRight == true)
            {
                rb.AddForce(new Vector2(750, 0));
            }
            else
            {
                rb.AddForce(new Vector2(-750, 0));
            }
        }

        //Flips the player character horizontally when the player moves left or right
        if (xMovement < 0)
        {
            transform.localScale = new Vector2(-scale, transform.localScale.y);
            facingRight = false;
        }
        else if (xMovement > 0)
        {
            transform.localScale = new Vector2(scale, transform.localScale.y);
            facingRight = true;
        }

        //Additional force is only applied if the player is below the maximum speed
        if (rb.velocity.magnitude < maxSpeed)
        {
            Vector2 movement = new Vector2(xMovement, 0);

            //Applies force to the player rigidbody
            //Direction is based on the input control pressed, acceleration is based on the movementScale variable
            rb.AddForce(movementScale * movement);
        }

        //Applies force to the rigidbody whenever the jump input is used
        //Can only jump when grounded
        if (Input.GetButtonDown("Jump") && isGrounded == true)
        {
            int horizontalForce;

            //Horizontal force is applied to the rigidbody based on the direction the player is facing
            //Only applied if not already at the max speed
            if (facingRight && rb.velocity.magnitude < maxSpeed)
            {
                horizontalForce = 250;
            }
            else if (!facingRight && rb.velocity.magnitude < maxSpeed)
            {
                horizontalForce = -250;
            }
            else
            {
                horizontalForce = 0;
            }

            Vector2 jumpForce = new Vector2(horizontalForce, jumpScale);
            rb.AddForce(jumpForce);
        }

        //Enforces the max speed each update
        if (rb.velocity.magnitude > maxSpeed)
        {
            Vector2 movement = new Vector2(xMovement, 0);

            rb.AddForce(-2 * movementScale * movement);
        }
    }

    //Marks the player as grounded when in contact with the ground
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "FloorTiles")
        {
            isGrounded = true;
        }
    }

    //Marks the player as not being grounded when in the air
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.name == "FloorTiles")
        {
            isGrounded = false;
        }
    }
}
