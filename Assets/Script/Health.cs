using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header ("Health")]
    public Bar healthBar;
    public float maxHelth = 100;
    public bool dead;
    private float currentHealth;
    private Animator anim;

    [Header("iFrames")]
    [SerializeField] private float iFrameDuration = 0.0f;
    [SerializeField] private float numberOfFlashes = 0.0f;
    private SpriteRenderer spriteRend;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();

        
        currentHealth = maxHelth;
        
        
        if (this.gameObject.tag == "player")
        {
            healthBar.SetMaxValue(maxHelth);
        }
    }

    public void TakeDamage (float _damage)
    {
        currentHealth -= _damage;
        
        if (this.gameObject.tag == "player")
            healthBar.SetValue(currentHealth);

        if(currentHealth > 0)
        {
            StartCoroutine(Invunerability());
            // hurt
        }else if(!dead)
        {
            if (this.gameObject.tag == "player")
                this.GetComponent<PlayerMovement>().enabled = false;
                this.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            if (this.gameObject.tag == "enemy")
                this.GetComponent<EnemyController>().enabled = false;

            anim.SetTrigger("die");       

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

    public void addHealth(float value)
    {
        currentHealth += value;
        if(currentHealth > maxHelth)
        {
            currentHealth = maxHelth;
        }
        healthBar.SetValue(currentHealth);
    }

    private void PlayerSkills_OnSkillUnlocked(object sender, PlayerSkills.OnSkillUnlockedEventArgs e)
    {
        switch(e.skillType)
        {
            case PlayerSkills.SkillType.HealthMax :
                maxHelth *= 1.5f;
                healthBar.SetMaxValue(maxHelth);
                break;
        }
    }
}
