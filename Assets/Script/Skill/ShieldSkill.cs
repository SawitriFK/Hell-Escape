using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldSkill : MonoBehaviour
{
    public float riseTimer;

    public Transform midlePos;
    public GameObject shield;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public IEnumerator Rise()
    {

        Instantiate(shield, midlePos.position, Quaternion.identity);

        yield return new WaitForSeconds(riseTimer);
    }
}
