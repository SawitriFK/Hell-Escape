using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public float damageFireball;
    public float dieTime = 4f;
    private Animator anim;
    // Start is called before the first frame update
    private float thrust = 5f;
    void Start()
    {
        anim = GetComponent<Animator>();
        FindObjectOfType<AudioManager>().Play("PlayerFireball");
        StartCoroutine(Timer());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer != LayerMask.NameToLayer("Player"))
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                Rigidbody2D enemy = collision.GetComponent<Rigidbody2D>();
                if(enemy != null){
                    enemy.isKinematic = false;
                    Vector2 difference = new Vector2(enemy.transform.position.x - transform.position.x, 0);
                    difference = difference.normalized * thrust;
                    enemy.AddForce(difference, ForceMode2D.Impulse);
                    enemy.isKinematic = true;
                    collision.GetComponent<Health>().TakeDamage(damageFireball);

                }



                
            }
            Die();
        } 

    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(dieTime);
        Die();
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
