using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player2Controller : MonoBehaviour
{   
    // Player Parameters
    [SerializeField] private float maxSpeed = 5;
    [SerializeField] private float jumpTakeOffSpeed = 12;
	[SerializeField] private float climbSpeed = 2;
    //Components
    protected Rigidbody2D myRigidbody2D;
    protected SpriteRenderer spriteRenderer;
    protected Animator anim;
    
    // GroundCheck Parameters
    protected bool grounded = false;
    [SerializeField] private Transform groundCheck;
    float groundRadius = 0.1f;
    [SerializeField] private LayerMask whatIsGround;

	// Movement
    private Vector2 move = Vector2.zero;
    private Vector2 velocity = Vector2.zero;
	public bool isOnLadder = false;
	private float gravity;
	private float climbVelocity;

	// For the text
	private int count;
	public Text countText;  //Store a reference to the UI Text component which will display the number of pickups collected.

	// This function will enable all components in the start
    void OnEnable()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        myRigidbody2D = GetComponent<Rigidbody2D>();
		count = 0;
		SetCountText ();
		gravity = myRigidbody2D.gravityScale;
    }

    void FixedUpdate()
    {
        // Updating Grounded
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);      // Generates parameters to bet attached to an GameObject who will check if there is any collision;
        anim.SetBool("grounded", grounded);

        // x axis

        move.x = Input.GetAxis("Horizontal");
        velocity.x = move.x * maxSpeed;

        // y axis
        if (Input.GetButtonDown("Jump") && grounded)
        {
            velocity.y = jumpTakeOffSpeed;
        }
        else if (Input.GetButtonUp("Jump"))
        {
            if (velocity.y > 0)
            {
                velocity.y = velocity.y * 0.5f;
            }
        }

		if(isOnLadder)
		{	anim.SetBool ("OnLadder", true);
			myRigidbody2D.gravityScale = 0;
			climbVelocity = Input.GetAxis("Vertical") * climbSpeed;
			myRigidbody2D.velocity = new Vector2 (velocity.x, climbVelocity);
			anim.SetFloat ("climbing", Mathf.Abs(myRigidbody2D.velocity.y) / climbSpeed);
		}
		else if(!isOnLadder)
		{	anim.SetBool ("OnLadder", false);
			myRigidbody2D.gravityScale = gravity;
			myRigidbody2D.velocity = new Vector2(velocity.x, myRigidbody2D.velocity.y + velocity.y); 

		}
			

          // Velocity of player in (X, Y) axis
        anim.SetFloat("Run", Mathf.Abs(myRigidbody2D.velocity.x) / maxSpeed);


        // Flipping Sprites
        bool flipSprite = (spriteRenderer.flipX ? (move.x > 0) : (move.x < 0));
        if (flipSprite)
        {
            spriteRenderer.flipX = !spriteRenderer.flipX;
        }

        velocity = Vector2.zero;     // After All changes, we need to reinicialize velocity.
    }

	// This function will set to inactive the pickups which touches the enemy.
	void OnTriggerEnter2D (Collider2D other)
	{
		if(other.gameObject.CompareTag("Pickup"))
		{		
			other.gameObject.SetActive(false);
			count += 100;
			SetCountText ();
		}
	}

	//This function updates the text displaying the number of objects
	void SetCountText()
	{
		//Set the text property of our our countText object to "Count: " followed by the number stored in our count variable.
		countText.text = "Points: " + count.ToString ();

	}
}
