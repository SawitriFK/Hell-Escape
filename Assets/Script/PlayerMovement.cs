using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speedWalk;
    [SerializeField] private float speedJump;
    [SerializeField] private Animator animator;
    [SerializeField]private LayerMask groundLayer;



    private Rigidbody2D body;
    private CapsuleCollider2D boxCollider;
    private bool facingRight = true;
    private float moveInput;

    private int extraJumps;
    [SerializeField]private int extraJumpsValue;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<CapsuleCollider2D>();

        extraJumps = extraJumpsValue;
    }

    private void Update()
    {
        Move();
        
        Jump();

        Facing();

        animator.SetBool("isWalk", moveInput != 0);
    }

    private void Move()
    {

        moveInput = Input.GetAxis("Horizontal");
        body.velocity = new Vector2(moveInput * speedWalk, body.velocity.y);

    }

    private void Facing()
    {
        if (moveInput > 0.01f)
            transform.localScale = Vector3.one;
        else if (moveInput < -0.01f)
            transform.localScale = new Vector3(-1, 1, 1);
    }

    private void Jump()
    {
        if (isGrounded())
        {
            extraJumps = extraJumpsValue;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) && extraJumps > 0)
        {
            body.velocity = new Vector2(body.velocity.x, speedJump);
            extraJumps--;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) && extraJumps == 0 && isGrounded())
        {
            body.velocity = new Vector2(body.velocity.x, speedJump);
        }
    }

    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }

}
