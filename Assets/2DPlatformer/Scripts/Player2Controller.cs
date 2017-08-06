using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2Controller : MonoBehaviour
{   
    // Player Parameters
    [SerializeField] private float maxSpeed = 5;
    [SerializeField] private float jumpTakeOffSpeed = 12;

    //Components
    protected Rigidbody2D rb2d;
    protected SpriteRenderer spriteRenderer;
    protected Animator anim;
    
    // GroundCheck Parameters
    protected bool grounded = false;
    [SerializeField] private Transform groundCheck;
    float groundRadius = 0.2f;
    [SerializeField] private LayerMask whatIsGround;
    private Vector2 move = Vector2.zero;
    private Vector2 velocity = Vector2.zero;
    private float Vo = 0;
    // for the Ladder
    public bool IsOnLadder = false;
    private bool gravityON = true;
    private float gravityModifier;
    public void SetNoGravity()
    {
        gravityON = false;
        rb2d.gravityScale = 0f;
    }
    public void ResetGravity()
    {
        gravityON = true;
        rb2d.gravityScale = gravityModifier;
    }

    void OnEnable()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        gravityModifier = rb2d.gravityScale;                                                        // Keeps the value of gravity which the operator set at the start;
    }

    void FixedUpdate()
    {
        // Updating Grounded
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);       // Generates parameters to bet attached to an GameObject who will check if there is any collision;
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




        if (IsOnLadder && Input.GetButton("Use"))
        {
            anim.SetBool("OnLadder", true);
            SetNoGravity();
            move.y = Input.GetAxis("Vertical");
            velocity.y = move.y * maxSpeed;
            Vo = rb2d.velocity.y;
        }
        else if (Input.GetButton("Use") || IsOnLadder == false || Input.GetButton("Jump"))
        {
            anim.SetBool("OnLadder", false);
            ResetGravity();
            Vo = 0;
        }
        if (gravityON)
        {
            rb2d.velocity = new Vector2(velocity.x, rb2d.velocity.y - Vo + velocity.y);                                        // Velocity of player in (X, Y) axis
        }
        anim.SetFloat("Run", Mathf.Abs(rb2d.velocity.x) / maxSpeed);


        // Flipping Sprites
        bool flipSprite = (spriteRenderer.flipX ? (move.x > 0) : (move.x < 0));
        if (flipSprite)
        {
            spriteRenderer.flipX = !spriteRenderer.flipX;
        }

        velocity = Vector2.zero;                                                                                      // After All changes, we need to reinicialize velocity.
    }
}
