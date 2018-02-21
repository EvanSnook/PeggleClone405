using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ball_collide : MonoBehaviour {

    private GameObject game_manager;

    private void Start()
    {
        game_manager = GameObject.FindGameObjectWithTag("game_manager");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Ball collided with " + collision.gameObject.tag);
        game_manager.SendMessage("BallCollision", collision);
    }
}
