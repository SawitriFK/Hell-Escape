using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController: MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] protected float speedWalk = 0.0f;
    [SerializeField] protected Animator animator;
    [SerializeField] protected LayerMask groundLayer;
    [SerializeField] protected Transform groundDetect = null;
    [SerializeField] protected int disRay = 0;
    [SerializeField] protected float patrolDistance = 0.0f;

    [Header("Attack Parameters")]
    [SerializeField] protected float attackCooldown = 0.0f;
    [SerializeField] private float range = 0.0f;
    [SerializeField] protected int damage = 0;
    [SerializeField] protected float curseDamage = 0.0f;

    [Header("Collider Parameters")]
    [SerializeField] private float colliderDistance = 0.0f;
    [SerializeField] protected CapsuleCollider2D capsuleCollider = null;

    [Header("Player Layer")]
    [SerializeField] public LayerMask playerLayer;
    protected float cooldownTimer = Mathf.Infinity;

    [Header("Drop Item")]
    public GameObject Coin;
    [Range(0, 1f)][SerializeField] protected float dropChance = 0.0f;

    [Header("Shield Damage")]
    [SerializeField]protected SkillActive skillActive;

    protected Health playerHealth;
    protected Curse playerCurse;
    protected PlayerMovement playerMovement;

    protected Rigidbody2D body;
    protected CapsuleCollider2D boxCollider;
    protected bool facingRight;
    protected GameObject target;
    protected Vector3 wayPointPos;
    protected string attackAnim;

    public bool ry;
    public bool gd;


    protected virtual void Start()
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
        skillActive = target.GetComponent<SkillActive>();
        
    }

    protected virtual void Update()
    {
        cooldownTimer += Time.deltaTime;
        if (PlayerInRange() && cooldownTimer >= attackCooldown)
        {
            Attack();
            cooldownTimer = 0;
        }else
        {
            Move();
        }

        ry = isColli();
        gd = isGrounded();
    }

    protected void Flip()
    {
        Vector2 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    protected bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.Raycast(groundDetect.position, Vector2.down, disRay, groundLayer);
        return raycastHit.collider != null;
    }

    protected bool isColli()
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

    protected virtual bool PlayerInRange()
    {
        RaycastHit2D hit =
            Physics2D.BoxCast(capsuleCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(capsuleCollider.bounds.size.x * range, capsuleCollider.bounds.size.y, capsuleCollider.bounds.size.z),
            0, Vector2.left, 0, playerLayer);

        return hit.collider != null;
    }

    protected virtual void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(capsuleCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(capsuleCollider.bounds.size.x * range, capsuleCollider.bounds.size.y, capsuleCollider.bounds.size.z));
        
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(patrolDistance, 0f,0f));
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(-patrolDistance, 0f,0f));
    }

    protected void DamagePlayer()
    {
        if(PlayerInRange() && !playerMovement.invinsible)
        {
            if(skillActive.shieldActive == false)
            {
                playerHealth.TakeDamage(damage);
                playerCurse.TakeCurseDamage(curseDamage);
            }

        }
    }

    protected void DropCoin()
    {
        float rand = Random.Range(0f, 1f);
  
        if(rand <= dropChance)
        {
            var dropItem = Instantiate(Coin, transform.position, transform.rotation);
        }
    }

    protected void Move()
    {
        if(checkAttackAnimRunning() || PlayerInRange())
        {
            return;
        }

        if (!isGrounded() || isColli())
        {

            if(!GameManager.playerDead)
            {

                float dir = target.transform.position.x - transform.position.x;
                if (facingRight)
                {

                    if(dir > 0 && Mathf.Abs(dir) < patrolDistance)
                    {

                        return;
                    }
                    
                    
                    facingRight = false;
                    Flip();

                }
                else
                {

                    if(dir < 0 && Mathf.Abs(dir) < patrolDistance)
                    {

                        return;
                    }
                    
                    facingRight = true;
                    Flip();
                    


                }
            }else
            {

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
            }
        }
        else if(!GameManager.playerDead && Vector2.Distance(transform.position, target.transform.position) < patrolDistance)
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
            }
            else if(posThisFrame.x < posLastFrame.x && facingRight)
            {
                facingRight = false;
                Flip();
            }
        }
        else
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

    protected virtual void Attack()
    {
        animator.SetBool("isMove", false);
        FindObjectOfType<AudioManager>().Play("EnemyAttack");
        animator.SetTrigger("attack");
        body.velocity = new Vector2(0.0f, body.velocity.y);
    }

    protected virtual bool checkAttackAnimRunning()
    {
        return animator.GetCurrentAnimatorStateInfo(0).IsName(attackAnim);
    }
}
