using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cyclone : MonoBehaviour
{
    public float dieTime;
    public float spinSpeed;

    public Transform[] wapPoints;
    private int _curentWP;
    Transform nextPos;
    void Start()
    {
        FindObjectOfType<AudioManager>().Play("PlayerCyclone");
        StartCoroutine(Timer());
        
        nextPos = wapPoints[0];
    }

    private void Update()
    {
        MoveCyclone();
    }

    void MoveCyclone()
    {
        if(transform.position == nextPos.position)
        {
            _curentWP++;
            if(_curentWP >= wapPoints.Length)
            {
                _curentWP = 0;
            }
            nextPos = wapPoints[_curentWP];
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, nextPos.position, spinSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer != LayerMask.NameToLayer("Player"))
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                collision.GetComponent<Health>().TakeDamage(200);
            }

            
        }

    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(dieTime);
        Die();
    }

    void Die()
    {
        FindObjectOfType<AudioManager>().Stop("PlayerCyclone");
        Destroy(gameObject);
    }
}
