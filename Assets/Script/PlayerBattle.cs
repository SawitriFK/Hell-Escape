using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBattle : MonoBehaviour
{
    [Header("Attack Parameters")]
    [SerializeField] private float range = 0.0f;
    [SerializeField] private int damage = 0;
    [SerializeField] public LayerMask enemyLayer;

    [Header("Collider Parameters")]
    [SerializeField] private float colliderDistance = 0.0f;
    [SerializeField] private CapsuleCollider2D capsuleCollider = null;

    private Health enemyHealth;
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void Attack()
    {
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("PlayerFight"))
        {
            if(anim.GetBool("isWalk"))
            {
                anim.SetBool("isWalk", false);
            }
            anim.SetTrigger("attack");
        }
    }

    private void DamageEnemy()
    {
        if (EnemyInSight())
            enemyHealth.TakeDamage(damage);
    }

    private bool EnemyInSight()
    {
        RaycastHit2D hit =
            Physics2D.BoxCast(capsuleCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(capsuleCollider.bounds.size.x * range, capsuleCollider.bounds.size.y, capsuleCollider.bounds.size.z),
            0, Vector2.left, 0, enemyLayer);

        if (hit.collider != null)
            enemyHealth = hit.transform.GetComponent<Health>();

        return hit.collider != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(capsuleCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(capsuleCollider.bounds.size.x * range, capsuleCollider.bounds.size.y, capsuleCollider.bounds.size.z));
    }
}
