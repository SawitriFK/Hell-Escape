using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CycloneSkill : MonoBehaviour
{
    public float spinSpeed, spinTimer;

    public bool isReverse;
    public Transform midlePos;
    public GameObject cyclone;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            StartCoroutine(Spin());
        }

    }


    public IEnumerator Spin()
    {
        int direction()
        {
            if (transform.localScale.x < 0f)
            {
                return 180;
            }
            else
            {
                return 0;
            }
        }

        GameObject newCyclone = Instantiate(cyclone, midlePos.position, Quaternion.identity);
        newCyclone.transform.Rotate(0, 0, direction());




        yield return new WaitForSeconds(spinTimer);
    }
}
