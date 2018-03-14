using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class game_manager : MonoBehaviour {

    public int balls = 3;
    private GameObject launcher;
    private GameObject[] hit_blocks;
    public GameObject explosion_animation;
    private Vector3 block_position;

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
        collision.gameObject.tag = "hit";
        collision.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        //Destroy(collision.gameObject);
    }

    public void ResetBall(Collider2D ball)
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
    
    public void ExplodeBlocks()
    {
        hit_blocks = GameObject.FindGameObjectsWithTag("hit");

        foreach (GameObject block in hit_blocks)
        {
            block_position = new Vector3(block.transform.position.x, block.transform.position.y, block.transform.position.z);

            Destroy(block.gameObject);

            GameObject xplosion = Instantiate(explosion_animation,block_position, new Quaternion(0,0,0,0));

            StartCoroutine(WaitForAnimation(xplosion));

        }



    }

    IEnumerator WaitForAnimation(GameObject xplosion)
    {
        //time it takes explosion animation to complete
        yield return new WaitForSeconds(1F);

        Destroy(xplosion);
    }
}
