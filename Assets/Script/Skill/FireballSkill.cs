using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballSkill : MonoBehaviour
{
    public float shootSpeed, shootTimer;

    private bool isShooting;
    public Transform shootPos;
    public GameObject fireball;
   


    void Start()
    {
        isShooting = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            StartCoroutine(Shoot());
        }
    }

    public IEnumerator Shoot()
    {
        Transform _shootPos = shootPos;
        GameObject _fireball = fireball;
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
        Animator animator = newFireball.GetComponent<Animator>();

        newFireball.GetComponent<Rigidbody2D>().velocity = new Vector2(shootSpeed * direction() * Time.fixedDeltaTime, 0f);
        newFireball.transform.localScale = new Vector2(newFireball.transform.localScale.x * direction(), newFireball.transform.localScale.y);

        yield return new WaitForSeconds(shootTimer);
        isShooting = false;
    }

    
}
