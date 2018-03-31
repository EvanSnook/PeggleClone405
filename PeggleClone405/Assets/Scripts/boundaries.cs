using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//sends messages to the game_manager if the ball leaves the boundaries
public class boundaries : MonoBehaviour {

    private GameObject game_manager;

    private void Start()
    {
        game_manager = GameObject.Find("GameManager");
    }

    //resets the ball and then triggers the blocks to explode
    void OnTriggerExit2D(Collider2D ball)
    {
        Debug.Log("Ball left boundaries");
        game_manager.SendMessage("ResetBall", ball);

        Debug.Log("exploding blocks");
        game_manager.SendMessage("ExplodeBlocks");
    }
}
