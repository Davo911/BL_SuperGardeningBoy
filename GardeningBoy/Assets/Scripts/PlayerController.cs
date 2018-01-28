using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public GameObject plant;
    public Splatter splatter;
    public float maxSpeed = 4;
    public float jumpForcemax= 3f;
	public float jumpForcemin = 6f;
    public float jumpPushForce = 10f;
    public Transform groundCheck;
    public float distance = 1f;
    public LayerMask whatIsGround;
    public LayerMask whatIsWall;

    [HideInInspector]
    public bool lookingRight = true;

    private ParticleSystem particles;
    private Rigidbody2D rb2d;
    private Animator anim;
    private bool isGrounded = false;
    private bool touchingWall = false;
    private bool jump = false;
	private bool jumpStop = false;
    private bool moving = false;

	// Use this for initialization
	void Start () {
        //Zuweisung der Components
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        particles = GetComponent<ParticleSystem>();

    }
	
	// Update is called once per frame
	void Update () {
		Physics2D.queriesStartInColliders = false;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right * transform.localScale.x, distance);


		isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.15f, whatIsGround);

		if (Input.GetButtonDown("Jump") && isGrounded)
			jump = true;
		if (Input.GetButtonUp("Jump") && !isGrounded)
			//jumpStop = true;

        //WALLJUMP
        if (Input.GetButtonDown("Jump") && !hit.collider && !isGrounded)
        {
            rb2d.AddForce(new Vector2(jumpPushForce, jumpForcemin));
        }
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

        anim.SetBool("isGrounded", isGrounded);

        if ((hor > 0 && !lookingRight) || (hor < 0 && lookingRight))
            Flip();

		/*
        if (jump)
        {
            rb2d.AddForce(new Vector2(0, jumpForce));
            jump = false;
        }*/
		if (jump) {
			rb2d.velocity = new Vector2 (rb2d.velocity.x, jumpForcemax);
			jump = false;
		}
		if (jumpStop) {
			if (rb2d.velocity.y > jumpForcemin) {
				rb2d.velocity = new Vector2 (rb2d.velocity.x, jumpForcemin);
			}
			jumpStop = false;
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
        Vector3 temp = new Vector3(other.transform.position.x, other.transform.position.y, other.transform.position.z);
        if (other.CompareTag("Surface"))
        {
           temp.y+=0.2f;
           Instantiate(splatter, temp, Quaternion.Euler(0, 0, 0));
           Instantiate(plant,temp,Quaternion.Euler(0, 0, 0));
        }
        if (other.CompareTag("WallLeft"))
        {
            temp.x += 0.2f;
            Instantiate(splatter, temp, Quaternion.Euler(0, 0, -90));
        }
        if (other.CompareTag("WallRight"))
        {
            temp.x -= 0.2f;
            Instantiate(splatter, temp, Quaternion.Euler(0, 0, 90));
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawLine(transform.position, transform.position + Vector3.right * transform.localScale.x * distance);

    }
}
