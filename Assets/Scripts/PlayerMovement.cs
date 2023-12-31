using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Animator anim;
    private BoxCollider2D coll;
    [SerializeField] LayerMask jumpableGround;
    private Rigidbody2D rb;
    private SpriteRenderer sprite;

    private float horizontalInput;
    [SerializeField] private float moveSpeed = 7.0f;
    [SerializeField] private float jumpForce = 14.0f;

    private enum MovementSpeed { idle, running, jumping, falling }

    [SerializeField] private AudioSource jumpSoundEffect;

    // Start is called before the first frame update
    private void Start()
    {
        anim = GetComponent<Animator>();
        coll = GetComponent<BoxCollider2D>();
        rb  = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    private void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpSoundEffect.Play();
        }

        UpdateAnimationState();
    }

    private void UpdateAnimationState()
    {
        MovementSpeed state;

        if (horizontalInput > 0f)
        {
            state = MovementSpeed.running;
            sprite.flipX = false;
        }
        else if (horizontalInput < 0f)
        {
            state = MovementSpeed.running;
            sprite.flipX = true;
        }
        else
        {
            state = MovementSpeed.idle;
        }

        if (rb.velocity.y > .1f)
        {
            state = MovementSpeed.jumping;
        }
        else if (rb.velocity.y < -.1f)
        {
            state = MovementSpeed.falling;
        }

        anim.SetInteger("state", (int)state);
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }
}
