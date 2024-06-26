using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]private float speed;
    private Rigidbody2D body;
    private Animator anim;
    private bool grounded;

    private void Awake() {
        //Reference for Rigidbody and Animation from object
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update() {
        float horizontalInput = Input.GetAxis("Horizontal");
        body.velocity = new Vector2(horizontalInput *speed, body.velocity.y);

        //Flip player Left-Right
        if (horizontalInput > 0.01f)
        {
            transform.localScale = Vector3.one;
        }
        else if (horizontalInput < -0.01f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        
        if(Input.GetKey(KeyCode.Space) && grounded)
        {
            Jump();
        }

        //set animation parameter
        anim.SetBool("run", horizontalInput != 0);
        anim.SetBool("grounded", grounded);

    }


    private void Jump()
    {
        body.velocity = new Vector2(body.velocity.x, speed);
        grounded = false;
    }
 
    //check player collided with ground
    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.tag == "Ground")
        {
            grounded = true;   
        }
         
    }
}
