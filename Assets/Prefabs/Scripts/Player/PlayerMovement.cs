using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    public float movementScale;
    public float jumpScale;
    public float maxSpeed;
    bool isGrounded;
    float scale;
    bool facingRight = true;
    float xMovement = 0;

    Animator animator;

    public ControllerInput controllerInput;
    public GameObject blockSprite;

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

        //Keyboard input
        //float xMovement = Input.GetAxis("Horizontal");

        //Joystick input
        if (controllerInput.state == 2)
        {
            xMovement = 1;
        }
        else if (controllerInput.state == 3)
        {
            xMovement = -1;
        }
        else
        {
            xMovement = 0;
        }

        //Applies horizontal force to the rigidbody when the player attacks unless already at max speed
        if (Input.GetButtonDown("Fire1") == true || controllerInput.state == 1 && rb.velocity.magnitude < maxSpeed)
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
        //Also stops the player from blocking while moving
        if (xMovement < 0)
        {
            transform.localScale = new Vector2(-scale, transform.localScale.y);
            facingRight = false;
            blockSprite.gameObject.SetActive(false);
        }
        else if (xMovement > 0)
        {
            transform.localScale = new Vector2(scale, transform.localScale.y);
            facingRight = true;
            blockSprite.gameObject.SetActive(false);
        }

        //Applies force for horizontal movement when controls are used
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
        if (Input.GetButtonDown("Jump") || controllerInput.state == 5 && isGrounded == true)
        {
            int horizontalForce;

            //Horizontal force is applied to the rigidbody based on the direction the player is facing
            //Only applied if not already at the max speed
            if (facingRight && rb.velocity.magnitude < maxSpeed)
            {
                horizontalForce = 100;
            }
            else if (!facingRight && rb.velocity.magnitude < maxSpeed)
            {
                horizontalForce = -100;
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

        //Ensures player can't block in the air
        if (isGrounded == false)
        {
            blockSprite.gameObject.SetActive(false);
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
