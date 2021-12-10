using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortaltoNext : MonoBehaviour
{
    private Collider2D col;
    [SerializeField] GameObject next;
    [SerializeField] GameObject before;

    private void Start()
    {
        col = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        enCollider();
    }

    private void enCollider()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            col.enabled = false;
            next.SetActive(true);
            before.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            col.enabled = true;
        }
    }
}
