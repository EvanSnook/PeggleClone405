using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class game_manager : MonoBehaviour {

    public int balls = 3;
    private GameObject launcher;

    private void Start()
    {
        launcher = GameObject.FindGameObjectWithTag("launcher");
    }

	// Update is called once per frame
	void Update () {
		
	}

    public void BallCollision(Collision2D collision)
    {
        Debug.Log("destroy " + collision.gameObject);
        Destroy(collision.gameObject);
    }

    public void BallOutOfBounds(Collider2D ball)
    {
        Destroy(ball.gameObject);
        balls --;
        Debug.Log(balls + " lives left");
        Debug.Log(GameObject.FindGameObjectsWithTag("peg").Length + " pegs left");

        if (balls > 0 && GameObject.FindGameObjectsWithTag("peg").Length > 0)
        {
            launcher.SendMessage("resetFire");
        }
        else
        {
            Debug.Log("Game Over");
        }
        
    }
}
