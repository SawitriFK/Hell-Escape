using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{

    public float damageMeteor;
    public float dieTime = 4f;
    private Animator anim;
    Rigidbody2D rb;
    // Start is called before the first frame update

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        FindObjectOfType<AudioManager>().Play("PlayerMeteorFall");
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
                rb.velocity = Vector2.zero;
                FindObjectOfType<AudioManager>().Stop("PlayerMeteorFall");
                FindObjectOfType<AudioManager>().Play("PlayerMeteorExplosion");
                anim.SetTrigger("explosion");
                collision.GetComponent<Health>().TakeDamage(damageMeteor);
   
            }

            if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
            {
                rb.velocity = Vector2.zero;
                FindObjectOfType<AudioManager>().Stop("PlayerMeteorFall");
                FindObjectOfType<AudioManager>().Play("PlayerMeteorExplosion");
                anim.SetTrigger("explosion");
   
            }

        } 

    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(dieTime);
        
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
