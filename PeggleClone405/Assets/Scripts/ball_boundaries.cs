using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ball_boundaries : MonoBehaviour {

    void OnTriggerExit2D(Collider2D ball)
    {
        Debug.Log("Ball left boundaries");
        //Destroy(ball.gameObject);
        //launcher.SendMessage("resetFire");
    }
}
