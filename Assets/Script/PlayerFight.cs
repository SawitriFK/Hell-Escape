using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFight : MonoBehaviour
{
    [Header("Attack Parameters")]
    [SerializeField] private float range;
    [SerializeField] private int damage;

    [Header("Collider Parameters")]
    [SerializeField] private float colliderDistance;
    [SerializeField] private CapsuleCollider2D capsuleCollider;

    [Header("Player Layer")]
    [SerializeField] private LayerMask enemyLayer;

    [SerializeField] private Animator animator;

    private Health enemyHealth;
    void Start()
    {
  
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Attack();
        }
    }

    private void Attack()
    {
        animator.SetTrigger("attack");
    }

    private void DamageEnemy()
    {
        if (PlayerInSight())
            enemyHealth.TakeDamage(damage);
    }

    private bool PlayerInSight()
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
