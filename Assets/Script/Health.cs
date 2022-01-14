using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    [Header ("Health")]
    public Bar healthBar;
    public float maxHelth = 100;
    [SerializeField]private bool boss = false;
    public bool dead;
    [SerializeField] private float currentHealth;
    public static float playerHealth = -1;
    public static float playerMaxHealth = -1;
    private Animator anim;

    [Header("iFrames")]
    [SerializeField] private float iFrameDuration = 0.0f;
    [SerializeField] private float numberOfFlashes = 0.0f;
    private SpriteRenderer spriteRend;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
        
        if (this.gameObject.tag == "player")
        {
            if(playerMaxHealth == -1)
            {
                playerMaxHealth = maxHelth;
            }else
            {
                maxHelth = playerMaxHealth;
            }
            healthBar.SetMaxValue(playerMaxHealth);
            if(playerHealth == -1)
            {
                currentHealth = maxHelth;
                playerHealth = currentHealth;
            }else
            {
                currentHealth = playerHealth;
                healthBar.SetValue(currentHealth); 
            }
        }else
        {
            if(boss)
            {
                healthBar = GameObject.Find("BossBar").GetComponent<Bar>();
                healthBar.SetMaxValue(maxHelth);
            }
            currentHealth = maxHelth;
        }
    }

    public void TakeDamage (float _damage)
    {
        if(GameManager.playerDead)
        {
            return;
        }
        currentHealth -= _damage;
        
        
        if (this.gameObject.tag == "player")
        {
            playerHealth -= _damage;
            healthBar.SetValue(currentHealth);
            FindObjectOfType<AudioManager>().Play("PlayerDamaged");
        }else if (this.gameObject.tag == "enemy")
        {
            if(boss)
            {
                if(SceneManager.GetActiveScene().buildIndex == 1)
                {
                    FindObjectOfType<AudioManager>().Play("MinotaurDamaged");
                }else
                {
                    FindObjectOfType<AudioManager>().Play("GrimreaperDamaged");
                }
            }else
            {
                FindObjectOfType<AudioManager>().Play("EnemyDamaged");
            }
        }

        if(boss)
        {
            healthBar.SetValue(currentHealth);
        }
            

        if(currentHealth > 0)
        {
            StartCoroutine(Invunerability());
            // hurt
        }else if(!dead)
        {
            if (this.gameObject.tag == "player")
            {
                this.GetComponent<PlayerMovement>().enabled = false;
                this.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                Curse.currentCurse = -1;
                Curse.currCurseLv = -1;
                playerHealth = -1;
                playerMaxHealth = -1;
                GameManager.playerDead = true;
                FindObjectOfType<AudioManager>().Play("PlayerDie");
                GameManager.whyDead = GameManager.CauseOfDeath.Health;
            }
            if (this.gameObject.tag == "enemy")
            {
                this.GetComponent<EnemyController>().enabled = false;
                if(boss)
                {
                    if(SceneManager.GetActiveScene().buildIndex == 1)
                    {
                        FindObjectOfType<AudioManager>().Play("MinotaurDie");
                    }else
                    {
                        FindObjectOfType<AudioManager>().Play("GrimreaperDie");
                    }
                }else
                {
                    FindObjectOfType<AudioManager>().Play("EnemyDie");
                }
            }
                

            anim.SetTrigger("die");       

            dead = true;
        }
    }

    private IEnumerator Invunerability()
    {
        if (this.gameObject.tag == "player")
            Physics2D.IgnoreLayerCollision(9, 10, true);


        for (int i = 0; i < numberOfFlashes; i++)
        {
            spriteRend.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(iFrameDuration/(numberOfFlashes * 2));
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(iFrameDuration / (numberOfFlashes * 2));
        }
        if (this.gameObject.tag == "player")
            Physics2D.IgnoreLayerCollision(9, 10, false);
    }

    public void addHealth(float value)
    {
        currentHealth += value;
        playerHealth += value;
        if (currentHealth > maxHelth)
        {
            currentHealth = maxHelth;
            playerHealth = maxHelth;
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

    public bool halfHP()
    {
        if(currentHealth <= maxHelth / 2)
        {
            return true;
        }
        return false;
    }
}
