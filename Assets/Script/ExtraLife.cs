using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraLife : MonoBehaviour
{
    private Rigidbody2D rb;
    private Collider2D col;
    public float maxValue = 10f;
    public Transform groundDetect;
    private bool activated =false;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        col = gameObject.GetComponent<CircleCollider2D>();
        rb.AddForce(new Vector2(0f, 300f));
    }

    void Update()
    {
        RaycastHit2D raycastHit = Physics2D.Raycast(groundDetect.position, Vector2.down, 0.5f);

        if(!activated && raycastHit.collider != null && raycastHit.collider.gameObject.layer == 8)
        {
            activated = true;
            rb.velocity = Vector2.zero;
            rb.bodyType = RigidbodyType2D.Kinematic;
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "player")
        {
            float val = Random.Range(0f, maxValue);
            collider.gameObject.GetComponent<Health>().addHealth(val);
            Destroy(gameObject);
        }
    }
}
