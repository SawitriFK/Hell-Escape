using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : EnemyController
{
    [Header("Movement Phase 2")]
    [SerializeField] protected float speedWalk2 = 0.0f;

    [Header("Attack Parameters Phase 2")]
    [SerializeField] protected float attackCooldown2 = 0.0f;
    [SerializeField] protected float range2 = 0.0f;
    [SerializeField] protected int damage2 = 0;
    [SerializeField] protected float curseDamage2 = 0.0f;

    [Header("Collider Parameters Phase 2")]
    [SerializeField] protected float colliderDistance2 = 0.0f;

    private bool attacking = false;
    public bool phase2 = false;
    private Health bossHealth; 

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

        cooldownTimer += Time.deltaTime;

        if (PlayerInRange() && cooldownTimer >= attackCooldown)
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
        float rand = Random.Range(0f,1f);
        if(rand > 0.5)
        {
            animator.SetTrigger("attack1");   
        }else
        {
            animator.SetTrigger("attack2"); 
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
        if(phase2)
        {
            Gizmos.DrawWireCube(capsuleCollider.bounds.center + transform.right * range2 * transform.localScale.x * colliderDistance2,
                new Vector3(capsuleCollider.bounds.size.x * range2, capsuleCollider.bounds.size.y, capsuleCollider.bounds.size.z));
        }else
        {
            Gizmos.DrawWireCube(capsuleCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
                new Vector3(capsuleCollider.bounds.size.x * range, capsuleCollider.bounds.size.y, capsuleCollider.bounds.size.z));
        }
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(patrolDistance, 0f,0f));
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(-patrolDistance, 0f,0f));
    }

    private void Upgrade()
    {
        attackCooldown = attackCooldown2 ;
        range = range2;
        damage = damage2;
        curseDamage = curseDamage2;
        speedWalk = speedWalk2;
        colliderDistance = colliderDistance2;
    }
}
