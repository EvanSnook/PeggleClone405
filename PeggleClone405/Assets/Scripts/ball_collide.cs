using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//sends a message to the game_manager when the ball collides with a gameobject
public class ball_collide : MonoBehaviour {

    private GameObject game_manager;

    private void Start()
    {
        game_manager = GameObject.Find("GameManager");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Ball collided with " + collision.gameObject.tag);
        game_manager.SendMessage("BallCollision", collision);
    }
}
