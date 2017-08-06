using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2Controller : PhysicsObjects
{

    [SerializeField] private float MaxSpeed = 5;
    [SerializeField] private float JumpTakeOffSpeed = 12;

    SpriteRenderer spriteRenderer;
    Animator anim;
    public bool IsOnLadder = false;

	void Awake () {
        spriteRenderer = GetComponent<SpriteRenderer> ();
        anim = GetComponent<Animator> ();
        
	}

    protected override void ComputeVelocity()
    {
        Vector2 move = Vector2.zero;

        move.x = Input.GetAxis("Horizontal");


        if (IsOnLadder)
        {
            anim.SetBool("OnLadder", true);
            velocity.y = Input.GetAxis("Vertical") * MaxSpeed;
            SetNoGravity();
        }
        else
        {
            anim.SetBool("OnLadder", false);
            ResetGravity();
        }
            

        if (Input.GetButtonDown("Jump") && grounded)
        {

            velocity.y = JumpTakeOffSpeed;
        }
        else if (Input.GetButtonUp("Jump"))
        {
            if (velocity.y > 0)
            {
                velocity.y = velocity.y * .5f;
            }
        }

        bool flipSprite = (spriteRenderer.flipX ? (move.x > 0) : (move.x < 0));
        if (flipSprite)
        {
            spriteRenderer.flipX = !spriteRenderer.flipX;
        }

        anim.SetBool("grounded", grounded);
        anim.SetFloat("Run", Mathf.Abs(velocity.x) / MaxSpeed);
        anim.SetFloat("velocity.y", velocity.y / JumpTakeOffSpeed);
        targetVelocity = move * MaxSpeed;
        
    }
}
