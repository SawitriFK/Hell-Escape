using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    /*    [Header("Patrol Points")]
        [SerializeField] private Transform leftEdge;
        [SerializeField] private Transform rightEdge;

        [Header("Enemy")]
        [SerializeField] private Transform enemy;

        [Header("Movement parameters")]
        [SerializeField] private float speed;
        private Vector3 initScale;
        private bool movingLeft;

        [Header("Idle Behaviour")]
        [SerializeField] private float idleDuration;
        private float idleTimer;

        [Header("Enemy Animator")]
        [SerializeField] private Animator anim;

        private void Awake()
        {
            initScale = enemy.localScale;
        }
        private void OnDisable()
        {
            anim.SetBool("isMove", false);
        }

        private void Update()
        {
            if (movingLeft)
            {
                if (enemy.position.x >= leftEdge.position.x)
                    MoveInDirection(-1);
                else
                    DirectionChange();
            }
            else
            {
                if (enemy.position.x <= rightEdge.position.x)
                    MoveInDirection(1);
                else
                    DirectionChange();
            }
        }

        private void DirectionChange()
        {
            anim.SetBool("isMove", false);
            idleTimer += Time.deltaTime;

            if (idleTimer > idleDuration)
                movingLeft = !movingLeft;
        }

        private void MoveInDirection(int _direction)
        {
            idleTimer = 0;
            anim.SetBool("isMove", true);

            //Make enemy face direction
            enemy.localScale = new Vector3(Mathf.Abs(initScale.x) * _direction,
                initScale.y, initScale.z);

            //Move in that direction
            enemy.position = new Vector3(enemy.position.x + Time.deltaTime * _direction * speed,
                enemy.position.y, enemy.position.z);
        }*/



    [SerializeField] private float speedWalk;
    [SerializeField] private Animator animator;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundDetect;
    [SerializeField] private int disRay;


    private Rigidbody2D body;
    private CapsuleCollider2D boxCollider;
    public bool facingRight;


    private void Start()
    {
        facingRight = true;
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<CapsuleCollider2D>();


    }

    private void Update()
    {

        body.velocity = new Vector2(speedWalk, body.velocity.y);
        //Debug.Log(isGrounded());
        if (isGrounded() == false || isColli() == true)
        {
            if (facingRight == true)
            {
                speedWalk *= -1f;
                transform.rotation = Quaternion.Euler(0f, 180f, 0f);
                facingRight = false;
            }
            else
            {
                speedWalk *= -1f;
                transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                facingRight = true;
            }
        }

        animator.SetBool("isMove", true);



        /*        Facing();
                Move();
        */

        //animator.SetBool("isWalk", moveInput != 0);
    }

    private void Move()
    {

        //body.velocity = new Vector2(speedWalk, body.velocity.y);

        if (facingRight)
        {
            body.velocity = new Vector2(speedWalk, body.velocity.y);
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }else{
            body.velocity = new Vector2(-speedWalk, body.velocity.y);
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }


    }

    private void Facing()
    {
        if (!isGrounded())
        {
            facingRight = false;
        }
    }


    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.Raycast(groundDetect.position, Vector2.down, disRay);
        return raycastHit.collider != null;
    }
    private bool isColli()
    {
        RaycastHit2D raycastHit = Physics2D.Raycast(groundDetect.position, Vector2.down, 0);
        return raycastHit.collider != null;
    }

}
