using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boundaries : MonoBehaviour {

    private GameObject game_manager;

    private void Start()
    {
        game_manager = GameObject.FindGameObjectWithTag("game_manager");
    }

    void OnTriggerExit2D(Collider2D ball)
    {
        Debug.Log("Ball left boundaries");
        game_manager.SendMessage("BallOutOfBounds", ball);
        //Destroy(ball.gameObject);
        //launcher.SendMessage("resetFire");
    }
}
