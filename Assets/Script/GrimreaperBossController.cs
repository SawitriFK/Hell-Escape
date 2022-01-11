using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrimreaperBossController : EnemyController
{
    public enum AttackVariant {Attack, Special1, Special2};

    [Header("Boss Parameter")]
    [SerializeField] protected float speedWalkPhase2 = 0.0f;
    [SerializeField] protected float attackCooldownPhase2 = 0.0f;

    [Header("Boss Attack")]
    [SerializeField] protected int damageAttack = 0;
    [SerializeField] protected float rangeAttack = 0.0f;
    [SerializeField] protected float curseDamageAttack = 0.0f;
    [SerializeField] protected float colliderDistanceAttack = 0.0f;
    [SerializeField] protected int damageSpecial1 = 0;
    [SerializeField] protected float rangeSpecial1 = 0.0f;
    [SerializeField] protected float curseDamageSpecial1 = 0.0f;
    [SerializeField] protected float colliderDistanceSpecial1 = 0.0f;
    [SerializeField] protected int damageSpecial2 = 0;
    [SerializeField] protected float rangeSpecial2 = 0.0f;
    [SerializeField] protected float curseDamageSpecial2 = 0.0f;
    [SerializeField] protected float colliderDistanceSpecial2 = 0.0f;

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
            animator.SetBool("phase2", true);
            Upgrade();
        }

        float rand = Random.Range(0f,1f);
        if(rand > 0.5)
        {
            attackNow = AttackVariant.Attack;
        }else
        {
            if(phase2)
            {
                attackNow = AttackVariant.Special2;
            }else
            {
                attackNow = AttackVariant.Special1;
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
            case AttackVariant.Attack :
                animator.SetTrigger("attack1");
                damage = damageAttack;
                curseDamage = curseDamageAttack;
                break;
            case AttackVariant.Special1 :
                animator.SetTrigger("attack2");
                damage = damageSpecial1;
                curseDamage = curseDamageSpecial1;
                break;
            case AttackVariant.Special2 :
                animator.SetTrigger("attack2");
                damage = damageSpecial2;
                curseDamage = curseDamageSpecial2;
                break; 
        }
    }

    protected override bool checkAttackAnimRunning()
    {
        bool running = false;
        if(animator.GetCurrentAnimatorStateInfo(0).IsName("GrimreaperAttack"))
        {
            running = true;
        }else if(animator.GetCurrentAnimatorStateInfo(0).IsName("GrimreaperSpecial1"))
        {
            running = true;
        }else if(animator.GetCurrentAnimatorStateInfo(0).IsName("GrimreaperSpecial2"))
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
            case AttackVariant.Attack :
                Gizmos.DrawWireCube(capsuleCollider.bounds.center + transform.right * rangeAttack * transform.localScale.x * colliderDistanceAttack,
                    new Vector3(capsuleCollider.bounds.size.x * rangeAttack, capsuleCollider.bounds.size.y, capsuleCollider.bounds.size.z));
                break;
            case AttackVariant.Special1 :
                Gizmos.DrawWireCube(capsuleCollider.bounds.center + transform.right * rangeSpecial1 * transform.localScale.x * colliderDistanceSpecial1,
                    new Vector3(capsuleCollider.bounds.size.x * rangeSpecial1, capsuleCollider.bounds.size.y, capsuleCollider.bounds.size.z));
                break;
            case AttackVariant.Special2 :
                Gizmos.DrawWireCube(capsuleCollider.bounds.center + transform.right * rangeSpecial2 * transform.localScale.x * colliderDistanceSpecial2,
                    new Vector3(capsuleCollider.bounds.size.x * rangeSpecial2, capsuleCollider.bounds.size.y, capsuleCollider.bounds.size.z));
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
            case AttackVariant.Attack :
                RaycastHit2D hit =
                    Physics2D.BoxCast(capsuleCollider.bounds.center + transform.right * rangeAttack * transform.localScale.x * colliderDistanceAttack,
                    new Vector3(capsuleCollider.bounds.size.x * rangeAttack, capsuleCollider.bounds.size.y, capsuleCollider.bounds.size.z),
                    0, Vector2.left, 0, playerLayer);
                return hit.collider != null;
            case AttackVariant.Special1 :
                RaycastHit2D hit1 =
                    Physics2D.BoxCast(capsuleCollider.bounds.center + transform.right * rangeSpecial1 * transform.localScale.x * colliderDistanceSpecial1,
                    new Vector3(capsuleCollider.bounds.size.x * rangeSpecial1, capsuleCollider.bounds.size.y, capsuleCollider.bounds.size.z),
                    0, Vector2.left, 0, playerLayer);
                return hit1.collider != null;
            case AttackVariant.Special2 :
                RaycastHit2D hit3 =
                    Physics2D.BoxCast(capsuleCollider.bounds.center + transform.right * rangeSpecial2 * transform.localScale.x * colliderDistanceSpecial2,
                    new Vector3(capsuleCollider.bounds.size.x * rangeSpecial2, capsuleCollider.bounds.size.y, capsuleCollider.bounds.size.z),
                    0, Vector2.left, 0, playerLayer);
                return hit3.collider != null;
            default :
                return false;
        }
    }
}
