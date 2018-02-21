using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class game_manager : MonoBehaviour {

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
        launcher.SendMessage("resetFire");
    }
}
