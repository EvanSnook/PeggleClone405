using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class game_manager : MonoBehaviour {

    public int balls = 3;
    private GameObject launcher;
    private GameObject[] hit_blocks;

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
        collision.gameObject.GetComponent<SpriteRenderer>().color = Color.black;
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
            //change colour back to original
            block.gameObject.GetComponent<SpriteRenderer>().color = Color.white;

            //start the animation
            block.GetComponent<Animator>().SetBool("Destroy", true);


            StartCoroutine(WaitForGlitch(block));
    
        }

        StartCoroutine(WaitForAnimation(hit_blocks));

    }

    IEnumerator WaitForGlitch(GameObject block)
    {
        //arbitrary time that stops my glitch of the block becoming large before the animation starts
        yield return new WaitForSeconds(0.2F);

        //disable the colider to stop and potential issues from arisung
        block.GetComponent<PolygonCollider2D>().enabled = false;

        //increase size of animation
        block.transform.localScale = new Vector3(2, 2, 1);
    }

    IEnumerator WaitForAnimation(GameObject[] hit_blocks)
    {
        //time it takes explosion animation to complete
        yield return new WaitForSeconds(1F);

        foreach (GameObject block in hit_blocks)
        {
            Destroy(block.gameObject);
        }
    }
}
