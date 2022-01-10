using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : EnemyController
{
    // [Header("Movement Phase 2")]
    // [SerializeField] protected float speedWalk2 = 0.0f;

    // [Header("Attack Parameters Phase 2")]
    // [SerializeField] protected float attackCooldown2 = 0.0f;
    // [SerializeField] protected float range2 = 0.0f;
    // [SerializeField] protected int damage2 = 0;
    // [SerializeField] protected float curseDamage2 = 0.0f;

    // [Header("Collider Parameters Phase 2")]
    // [SerializeField] protected float colliderDistance2 = 0.0f;
    public enum AttackVariant {Jab, Axe, Crush, Throw};

    [Header("Boss Parameter")]
    [SerializeField] protected float speedWalkPhase2 = 0.0f;
    [SerializeField] protected float attackCooldownPhase2 = 0.0f;

    [Header("Boss Attack")]
    [SerializeField] protected int damageJab = 0;
    [SerializeField] protected float rangeJab = 0.0f;
    [SerializeField] protected float curseDamageJab = 0.0f;
    [SerializeField] protected float colliderDistanceJab = 0.0f;
    [SerializeField] protected int damageAxe = 0;
    [SerializeField] protected float rangeAxe = 0.0f;
    [SerializeField] protected float curseDamageAxe = 0.0f;
    [SerializeField] protected float colliderDistanceAxe = 0.0f;
    [SerializeField] protected int damageCrush = 0;
    [SerializeField] protected float rangeCrush = 0.0f;
    [SerializeField] protected float curseDamageCrush = 0.0f;
    [SerializeField] protected float colliderDistanceCrush = 0.0f;
    [SerializeField] protected int damageThrow = 0;
    [SerializeField] protected float rangeThrow = 0.0f;
    [SerializeField] protected float curseDamageThrow = 0.0f;
    [SerializeField] protected float colliderDistanceThrow = 0.0f;

    [Header("Boss Attack Visualization")]
    public AttackVariant visualizeAttack;

    private bool attacking = false;
    private bool phase2 = false;
    private Health bossHealth;
    private AttackVariant attackNow; 

    protected override void Start()
    {
        base.Start();
        bossHealth = gameObject.GetComponent<Health>();
        GameObject.Find("Game Manager").GetComponent<GameManager>().allEnemySpawned();
    }

    protected override void Update()
    {
        if(bossHealth.halfHP() && !phase2)
        {
            phase2 = true;
            animator.SetTrigger("phase2");
            Upgrade();
        }

        float rand = Random.Range(0f,1f);
        if(rand > 0.5)
        {
            if(phase2)
            {
                attackNow = AttackVariant.Crush;
            }else
            {
                attackNow = AttackVariant.Jab;
            }
        }else
        {
            if(phase2)
            {
                attackNow = AttackVariant.Throw;
            }else
            {
                attackNow = AttackVariant.Axe;
            }
        }

        cooldownTimer += Time.deltaTime;

        if (!GameManager.playerDead && PlayerInRange() && cooldownTimer >= attackCooldown)
        {
            Attack();
            cooldownTimer = 0;
        }else
        {
            Move();
        }
    }

    protected override void Attack()
    {
        attacking = true;
        body.velocity = new Vector2(0.0f, body.velocity.y);
        animator.SetBool("isMove", false);
        switch(attackNow)
        {
            case AttackVariant.Jab :
                animator.SetTrigger("attack1");
                break;
            case AttackVariant.Crush :
                animator.SetTrigger("attack1");
                break;
            case AttackVariant.Axe :
                animator.SetTrigger("attack2");
                break;
            case AttackVariant.Throw :
                animator.SetTrigger("attack2");
                break; 
        }
    }

    protected override bool checkAttackAnimRunning()
    {
        bool running = false;
        if(animator.GetCurrentAnimatorStateInfo(0).IsName("MinotaurJab"))
        {
            running = true;
        }else if(animator.GetCurrentAnimatorStateInfo(0).IsName("MinotaurAxe"))
        {
            running = true;
        }else if(animator.GetCurrentAnimatorStateInfo(0).IsName("MinotaurCrush"))
        {
            running = true;
        }else if(animator.GetCurrentAnimatorStateInfo(0).IsName("MinotaurThrow"))
        {
            running = true;
        }
        return running;
    }

    protected override void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        switch(visualizeAttack)
        {
            case AttackVariant.Jab :
                Gizmos.DrawWireCube(capsuleCollider.bounds.center + transform.right * rangeJab * transform.localScale.x * colliderDistanceJab,
                    new Vector3(capsuleCollider.bounds.size.x * rangeJab, capsuleCollider.bounds.size.y, capsuleCollider.bounds.size.z));
                break;
            case AttackVariant.Axe :
                Gizmos.DrawWireCube(capsuleCollider.bounds.center + transform.right * rangeAxe * transform.localScale.x * colliderDistanceAxe,
                    new Vector3(capsuleCollider.bounds.size.x * rangeAxe, capsuleCollider.bounds.size.y, capsuleCollider.bounds.size.z));
                break;
            case AttackVariant.Crush :
                Gizmos.DrawWireCube(capsuleCollider.bounds.center + transform.right * rangeCrush * transform.localScale.x * colliderDistanceCrush,
                    new Vector3(capsuleCollider.bounds.size.x * rangeCrush, capsuleCollider.bounds.size.y, capsuleCollider.bounds.size.z));
                break;
            case AttackVariant.Throw :
                Gizmos.DrawWireCube(capsuleCollider.bounds.center + transform.right * rangeThrow * transform.localScale.x * colliderDistanceThrow,
                    new Vector3(capsuleCollider.bounds.size.x * rangeThrow, capsuleCollider.bounds.size.y, capsuleCollider.bounds.size.z));
                break;
        }
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(patrolDistance, 0f,0f));
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(-patrolDistance, 0f,0f));
    }

    private void Upgrade()
    {
        attackCooldown = attackCooldownPhase2 ;
        speedWalk = speedWalkPhase2;
    }

    protected override bool PlayerInRange()
    {
        switch(attackNow)
        {
            case AttackVariant.Jab :
                RaycastHit2D hit =
                    Physics2D.BoxCast(capsuleCollider.bounds.center + transform.right * rangeJab * transform.localScale.x * colliderDistanceJab,
                    new Vector3(capsuleCollider.bounds.size.x * rangeJab, capsuleCollider.bounds.size.y, capsuleCollider.bounds.size.z),
                    0, Vector2.left, 0, playerLayer);
                return hit.collider != null;
            case AttackVariant.Axe :
                RaycastHit2D hit1 =
                    Physics2D.BoxCast(capsuleCollider.bounds.center + transform.right * rangeAxe * transform.localScale.x * colliderDistanceAxe,
                    new Vector3(capsuleCollider.bounds.size.x * rangeAxe, capsuleCollider.bounds.size.y, capsuleCollider.bounds.size.z),
                    0, Vector2.left, 0, playerLayer);
                return hit1.collider != null;
            case AttackVariant.Crush :
                RaycastHit2D hit2 =
                    Physics2D.BoxCast(capsuleCollider.bounds.center + transform.right * rangeCrush * transform.localScale.x * colliderDistanceCrush,
                    new Vector3(capsuleCollider.bounds.size.x * rangeCrush, capsuleCollider.bounds.size.y, capsuleCollider.bounds.size.z),
                    0, Vector2.left, 0, playerLayer);
                return hit2.collider != null;
            case AttackVariant.Throw :
                RaycastHit2D hit3 =
                    Physics2D.BoxCast(capsuleCollider.bounds.center + transform.right * rangeThrow * transform.localScale.x * colliderDistanceThrow,
                    new Vector3(capsuleCollider.bounds.size.x * rangeThrow, capsuleCollider.bounds.size.y, capsuleCollider.bounds.size.z),
                    0, Vector2.left, 0, playerLayer);
                return hit3.collider != null;
            default :
                return false;
        }
    }
}
