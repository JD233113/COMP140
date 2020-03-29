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

    // Start is called before the first frame update
    void Start()
    {
        //Stores the rigidbody to reduce the number of times GetComponent() needs to be called
        rb = this.gameObject.GetComponent<Rigidbody2D>();

        scale = transform.localScale.x;
    }

    void FixedUpdate()
    {
        //Checks whether the movement inputs are being used
        float xMovement = Input.GetAxis("Horizontal");

        if (xMovement < 0)
        {
            transform.localScale = new Vector2(-scale, transform.localScale.y);
        }
        else if (xMovement > 0)
        {
            transform.localScale = new Vector2(scale, transform.localScale.y);
        }

        //Additional force is only applied if the player is below the maximum speed
        if (rb.velocity.magnitude < maxSpeed)
        {
            Vector2 movement = new Vector2(xMovement, 0);

            //Applies force to the player rigidbody
            //Direction is based on the input control pressed, acceleration is based on the movementScale variable
            rb.AddForce(movementScale * movement);
        }

        //Applies positive force to the rigidbody on the Y axis whenever the jump input is used
        //Can only jump when grounded
        if (Input.GetButtonDown("Jump") && isGrounded == true)
        {
            Vector2 jumpForce = new Vector2(0, jumpScale);
            rb.AddForce(jumpForce);
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
