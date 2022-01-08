using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController: MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float speedWalk = 0.0f;
    [SerializeField] private Animator animator;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundDetect = null;
    [SerializeField] private int disRay = 0;
    [SerializeField] private float patrolDistance = 0.0f;

    [Header("Attack Parameters")]
    [SerializeField] private float attackCooldown = 0.0f;
    [SerializeField] private float range = 0.0f;
    [SerializeField] private int damage = 0;
    [SerializeField] private float curseDamage = 0.0f;

    [Header("Collider Parameters")]
    [SerializeField] private float colliderDistance = 0.0f;
    [SerializeField] private CapsuleCollider2D capsuleCollider = null;

    [Header("Player Layer")]
    [SerializeField] public LayerMask playerLayer;
    private float cooldownTimer = Mathf.Infinity;

    [Header("Drop Item")]
    public GameObject Coin;
    [Range(0, 1f)][SerializeField] private float dropChance = 0.0f;

    private Health playerHealth;
    private Curse playerCurse;
    private PlayerMovement playerMovement;

    private Rigidbody2D body;
    private CapsuleCollider2D boxCollider;
    private bool facingRight;
    private GameObject target;
    private Vector3 wayPointPos;
    private string attackAnim;


    private void Start()
    {
        float fRand = Random.Range(0.0f,1.0f);
        if(fRand > 0.5f)
        {
            facingRight = true;
        }else
        {
            facingRight = false;
            Flip();

        }

        speedWalk = Random.Range(speedWalk, speedWalk+1);
        
        target = GameObject.FindGameObjectsWithTag("player")[0];
        animator = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<CapsuleCollider2D>();
        playerHealth = target.GetComponent<Health>();
        playerCurse = target.GetComponent<Curse>();
        playerMovement = target.GetComponent<PlayerMovement>();
        attackAnim = gameObject.name.Replace("(Clone)","").Trim() + "Attack";
    }

    private void Update()
    {
        cooldownTimer += Time.deltaTime;

        //Attack only when player in sight?
        if (PlayerInRange())
        {
            animator.SetBool("isMove", false);
            
            if (cooldownTimer >= attackCooldown)
            {
                animator.SetTrigger("attack");
                cooldownTimer = 0;
            }

            body.velocity = new Vector2(0.0f, body.velocity.y);
        }else
        {
            if(animator.GetCurrentAnimatorStateInfo(0).IsName(attackAnim))
            {
                return;
            }

            if (!isGrounded() || isColli())
            {
                if(!playerHealth.dead)
                {
                    float dir = target.transform.position.x - transform.position.x;
                    if(facingRight && dir > 0)
                    {
                        return;
                    }else if(!facingRight && dir < 0)
                    {
                        return;
                    }
                }
                if (facingRight)
                {
                    facingRight = false;
                    Flip();
                }
                else
                {
                    facingRight = true;
                    Flip();
                }
            }else if(!playerHealth.dead && Vector2.Distance(transform.position, target.transform.position) < patrolDistance)
            {
                body.velocity = new Vector2(0.0f, body.velocity.y);
                wayPointPos = new Vector3(target.transform.position.x, transform.position.y, transform.position.z);

                Vector2 posLastFrame = transform.position;

                transform.position = Vector3.MoveTowards(transform.position, wayPointPos, speedWalk * Time.deltaTime);

                Vector2 posThisFrame = transform.position;
                
                if(posThisFrame.x > posLastFrame.x && !facingRight)
                {
                    facingRight = true;
                    Flip();
                }else if(posThisFrame.x < posLastFrame.x && facingRight)
                {
                    facingRight = false;
                    Flip();
                }
            }else
            {
                if(facingRight)
                {
                    body.velocity = new Vector2(speedWalk, body.velocity.y);
                }else
                {
                    body.velocity = new Vector2(-speedWalk, body.velocity.y);
                }
                
            }
            animator.SetBool("isMove", true);
        }
    }

    private void Flip()
    {
        Vector2 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.Raycast(groundDetect.position, Vector2.down, disRay);
        return raycastHit.collider != null;
    }

    private bool isColli()
    {
        RaycastHit2D raycastHit = Physics2D.Raycast(groundDetect.position, Vector2.down, 0);
        if(raycastHit.collider)
        {
            return raycastHit.collider.gameObject.layer != 10;
        }else
        {
            return false;
        }
        
    }

    private bool PlayerInRange()
    {
        RaycastHit2D hit =
            Physics2D.BoxCast(capsuleCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(capsuleCollider.bounds.size.x * range, capsuleCollider.bounds.size.y, capsuleCollider.bounds.size.z),
            0, Vector2.left, 0, playerLayer);

        return hit.collider != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(capsuleCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(capsuleCollider.bounds.size.x * range, capsuleCollider.bounds.size.y, capsuleCollider.bounds.size.z));
        
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(patrolDistance, 0f,0f));
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(-patrolDistance, 0f,0f));
    }

    private void DamagePlayer()
    {
        if(PlayerInRange() && !playerMovement.invinsible)
        {
            playerHealth.TakeDamage(damage);
            playerCurse.TakeCurseDamage(curseDamage);
        }
    }

    private void DropCoin()
    {
        float rand = Random.Range(0f, 1f);
  
        if(rand < dropChance)
        {
            var dropItem = Instantiate(Coin, transform.position, transform.rotation);
        }
    }
}
