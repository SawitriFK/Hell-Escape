using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public float dieTime;
    private Animator anim;
    private float thrust = 5f;

    protected GameObject target;
    protected Vector3 wayPointPos;

    void Start()
    {
        anim = GetComponent<Animator>();
        FindObjectOfType<AudioManager>().Play("PlayerShield");
        StartCoroutine(Timer());
        target = GameObject.FindGameObjectsWithTag("player")[0];
        
    }

    // Update is called once per frame
    void Update()
    {
        wayPointPos = new Vector3(target.transform.position.x, target.transform.position.y, transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, wayPointPos, 10 * Time.deltaTime);
    }

    // private void OnTriggerEnter2D(Collider2D collision)
    // {
    //     if (collision.gameObject.layer != LayerMask.NameToLayer("Player") && collision.gameObject.layer != LayerMask.NameToLayer("Ground"))
    //     {
    //         if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
    //         {

    //             Rigidbody2D enemy = collision.GetComponent<Rigidbody2D>();
    //             if(enemy != null){
    //                 // Debug.Log("Kena");
    //                 enemy.isKinematic = false;
    //                 Vector2 difference = new Vector2(enemy.transform.position.x - transform.position.x, 0);
    //                 difference = difference.normalized * thrust;
    //                 enemy.AddForce(difference, ForceMode2D.Impulse);
    //                 enemy.isKinematic = true;

    //             }




                
    //         }
    //         Die();
    //     }
    // }

    IEnumerator Timer()
    {

        yield return new WaitForSeconds(dieTime);
        anim.SetTrigger("close");
    }

    void Die()
    {
        FindObjectOfType<AudioManager>().Stop("PlayerShield");
        Destroy(gameObject);
    }
}
