using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public Splatter splatter;
    public float maxSpeed = 4;
    public float jumpForce = 550;
    public Transform groundCheck;
    public LayerMask whatIsGround;

    [HideInInspector]
    public bool lookingRight = true;

    private Rigidbody2D rb2d;
    private Animator anim;
    private bool isGrounded = false;
    private bool jump = false;
    private bool moving = false;

	// Use this for initialization
	void Start () {
        //Zuweisung der Components
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Jump") && isGrounded)
            jump = true;
	}

    //fixed Timestep
    private void FixedUpdate()
    {
        float hor = Input.GetAxis("Horizontal");
        anim.SetFloat("speed", Mathf.Abs(hor));
        float h = Input.GetAxisRaw("Horizontal");
        if (h > 0.01f)
        {
            moving = true;
        }
        else
        {
            moving = false;
        }
        rb2d.velocity = new Vector2(hor * maxSpeed,rb2d.velocity.y);

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.15f, whatIsGround);
        anim.SetBool("isGrounded", isGrounded);

        if ((hor > 0 && !lookingRight) || (hor < 0 && lookingRight))
            Flip();

        if (jump)
        {
            rb2d.AddForce(new Vector2(0, jumpForce));
            jump = false;
        }
    }

    public void Flip()
    {
        lookingRight = !lookingRight;
        Vector3 myScale = transform.localScale;
        myScale.x *= -1;
        transform.localScale = myScale;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Surface"))
        {
           Instantiate(splatter, other.transform.position, Quaternion.identity);
        }
    }
}
