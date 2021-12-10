using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header ("Helath")]
    public HealthBar healthBar;
    public int maxHelth = 100;
    public int currentHealth;
    public bool dead;
    private Animator anim;

    [Header("iFrames")]
    [SerializeField] private float iFrameDuration;
    [SerializeField] private float numberOfFlashes;
    private SpriteRenderer spriteRend;


    private void Awake()
    {
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();

        currentHealth = maxHelth;

        if (this.gameObject.tag == "player")
            healthBar.SetMaxHealth(maxHelth);
    }

/*    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            TakeDamage(10);
        }
    }*/

    public void TakeDamage (int _damage)
    {
        currentHealth -= _damage;
        
        if (this.gameObject.tag == "player")
            healthBar.SetHealth(currentHealth);

        if(currentHealth > 0)
        {
            StartCoroutine(Invunerability());
            // hurt
        }else if(!dead)
        {
            if (this.gameObject.tag == "player")
                this.GetComponent<PlayerMovement>().enabled = false;
            if (this.gameObject.tag == "enemy")
                this.GetComponent<EnemyPatrol>().enabled = false;

            //this.GetComponent<EnemyPatrol>().enabled = false;
            anim.SetTrigger("die");

            Destroy(gameObject, 1f);
/*
            if (this.gameObject.tag == "player")
                Destroy(this);
            if (this.gameObject.tag == "enemy")
                Destroy(this);*/

            dead = true;
        }
    }

    private IEnumerator Invunerability()
    {
        if (this.gameObject.tag == "player")
            Physics2D.IgnoreLayerCollision(9, 10, true);

        if (this.gameObject.tag == "enemy")
            Physics2D.IgnoreLayerCollision(10, 9, true);


        for (int i = 0; i < numberOfFlashes; i++)
        {
            spriteRend.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(iFrameDuration/(numberOfFlashes * 2));
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(iFrameDuration / (numberOfFlashes * 2));
        }
        if (this.gameObject.tag == "player")
            Physics2D.IgnoreLayerCollision(9, 10, false);
        if (this.gameObject.tag == "enemy")
            Physics2D.IgnoreLayerCollision(10, 9, false);
    }
}
