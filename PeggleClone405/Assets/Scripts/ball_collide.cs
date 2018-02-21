using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ball_collide : MonoBehaviour {

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Ball collided with " + collision.gameObject.tag);
    }
}
