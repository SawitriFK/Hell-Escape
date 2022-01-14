using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBattle : MonoBehaviour
{
    [Header("Attack Parameters")]
    [SerializeField] private float range = 0.0f;
    [SerializeField] private float range2 = 0.0f;
    [SerializeField] private int damage = 0;
    [SerializeField] private int damage2 = 0;
    [SerializeField] public LayerMask enemyLayer;

    [Header("Collider Parameters")]
    [SerializeField] private float colliderDistance = 0.0f;
    [SerializeField] private float colliderDistance2 = 0.0f;
    [SerializeField] private CapsuleCollider2D capsuleCollider = null;

    private Animator anim;
    private int countAttack = 0;
    private RaycastHit2D[] enemy;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void Attack()
    {
        if(anim.GetBool("isWalk"))
        {
            anim.SetBool("isWalk", false);
        }
        countAttack++;
        if(countAttack == 1)
        {
            FindObjectOfType<AudioManager>().Play("PlayerAttack1");
            anim.SetInteger("attack", 1);
        }
    }

    private void DamageEnemy()
    {
        if (EnemyInSight())
        {
            if(anim.GetCurrentAnimatorStateInfo(0).IsName("PlayerFight"))
            {
                foreach(RaycastHit2D e in enemy)
                {
                    e.collider.GetComponent<Health>().TakeDamage(damage);
                }
            }else
            {
                foreach(RaycastHit2D e in enemy)
                {
                    e.collider.GetComponent<Health>().TakeDamage(damage2);
                }
            }
        }
            
    }

    private bool EnemyInSight()
    {
        if(anim.GetCurrentAnimatorStateInfo(0).IsName("PlayerFight"))
        {
            RaycastHit2D[] hit =
            Physics2D.BoxCastAll(capsuleCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(capsuleCollider.bounds.size.x * range, capsuleCollider.bounds.size.y, capsuleCollider.bounds.size.z),
            0, Vector2.left, 0, enemyLayer);
            if (hit != null)
            {
                enemy = hit;
            }

            return hit != null;
        }else if(anim.GetCurrentAnimatorStateInfo(0).IsName("PlayerFight2"))
        {
            RaycastHit2D[] hit =
            Physics2D.BoxCastAll(capsuleCollider.bounds.center + transform.right * range2 * transform.localScale.x * colliderDistance2,
            new Vector3(capsuleCollider.bounds.size.x * range2, capsuleCollider.bounds.size.y, capsuleCollider.bounds.size.z),
            0, Vector2.left, 0, enemyLayer);

            if (hit != null)
            {
                enemy = hit;
            }
            
            return hit != null;
        }
        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(capsuleCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(capsuleCollider.bounds.size.x * range, capsuleCollider.bounds.size.y, capsuleCollider.bounds.size.z));
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(capsuleCollider.bounds.center + transform.right * range2 * transform.localScale.x * colliderDistance2,
            new Vector3(capsuleCollider.bounds.size.x * range2, capsuleCollider.bounds.size.y, capsuleCollider.bounds.size.z));
    }

    public void CheckAttack()
    {
        if(anim.GetCurrentAnimatorStateInfo(0).IsName("PlayerFight"))
        {
            if(countAttack > 1)
            {
                FindObjectOfType<AudioManager>().Play("PlayerAttack2");
                anim.SetInteger("attack", 2);
            }else
            {
                ResetAttackPhase();
            }
        }else if(anim.GetCurrentAnimatorStateInfo(0).IsName("PlayerFight2"))
        {
            if(countAttack >= 2)
            {
                ResetAttackPhase();
            }
        }
    }

    public void ResetAttackPhase()
    {
        countAttack = 0;
        anim.SetInteger("attack", 0);
    }
}
