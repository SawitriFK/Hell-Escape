using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillActive : MonoBehaviour
{
    [Header("Fireball")]

    public float shootSpeed;
    public float shootTimer;
    public float manaFireball;
    private bool isShooting;
    public Transform shootPos;
    public GameObject fireball;

    [Header("Cyclone")]
    public float spinSpeed;
    public float spinTimer;
    public float manaCyclone;
    public bool isReverse;
    public Transform midlePos;
    public GameObject cyclone;

    [Header("Shield")]
    public float riseTimer;
    public float manaShield;
    public Transform pointShield;
    public GameObject shield;
    public bool shieldActive = false;

    [Header("Meteor")]

    public float meteorSpeed;
    public float meteorTimer;
    public float manaMeteor;
    private bool isFall;
    public Transform meteorPos;
    public List<Transform> mPos;
    public System.Random rnd = new System.Random();
    public GameObject meteor;
    
    [Header("Set Mana")]
    public Mana manaSkill;

   
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private bool setMana(float _mana){
        bool manaBool = manaSkill.GetComponent<Mana>().UseSkill(_mana);
        return manaBool;
    }
    
    public IEnumerator Shoot()
    {
        if(setMana(manaFireball) == true){

            int direction()
            {
                if(transform.localScale.x < 0f)
                {
                    return -1;
                }
                else
                {
                    return +1;
                }
            }

            isShooting = true;
            GameObject newFireball = Instantiate(fireball, shootPos.position, Quaternion.identity);


            newFireball.GetComponent<Rigidbody2D>().velocity = new Vector2(shootSpeed * direction() * Time.fixedDeltaTime, 0f);
            newFireball.transform.localScale = new Vector2(newFireball.transform.localScale.x * direction(), newFireball.transform.localScale.y);

            yield return new WaitForSeconds(shootTimer);
            isShooting = false;

        }
        
    }
    public IEnumerator Spin()
    {
        if(setMana(manaCyclone) == true){
            int direction()
            {
                if (transform.localScale.x < 0f)
                {
                    return -180;
                }
                else
                {
                    return 0;
                }
            }

            GameObject newCyclone = Instantiate(cyclone, midlePos.position, Quaternion.identity);
            newCyclone.transform.Rotate(0, direction(), 0);




            yield return new WaitForSeconds(spinTimer);
        }
    }
    public IEnumerator Rise()
    {
        if(setMana(manaShield) == true){
            if(shieldActive == false){
                Instantiate(shield, pointShield.position, Quaternion.identity);
                
                shieldActive = true;
                yield return new WaitForSeconds(riseTimer);
                shieldActive = false;
            }
        }

    }

    public IEnumerator Explosion()
    {
        if(setMana(manaMeteor) == true){
            int direction()
            {
                if(transform.localScale.x < 0f)
                {
                    return -1;
                }
                else
                {
                    return +1;
                }
            }

            isFall = true;

            for (int i = 0; i < mPos.Count; i++){
                GameObject newMeteor = Instantiate(meteor, mPos[i].position, Quaternion.identity);
                newMeteor.GetComponent<Rigidbody2D>().velocity = new Vector2(meteorSpeed * direction() * Time.fixedDeltaTime, -50f);
            }


            yield return new WaitForSeconds(meteorTimer);
            isFall = false;

        }
        
    }
}
