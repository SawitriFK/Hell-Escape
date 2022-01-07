using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowX : MonoBehaviour
{
    public Transform player;
    public float smoothSpeed = 0.125f; 

    void FixedUpdate(){
        if(player == null)
        {
            return;
        }
        
        Vector2 targetPos = new Vector2(player.position.x, transform.position.y);
        Vector2 smoothedPosition = Vector2.Lerp(transform.position, targetPos, smoothSpeed);
        transform.position = smoothedPosition;
    }
}
