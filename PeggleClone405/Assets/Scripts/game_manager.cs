using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class game_manager : MonoBehaviour {

    public int balls = 3;
    private GameObject launcher;
    private GameObject[] hit_blocks;
    public GameObject explosion_animation;
    private Vector3 block_position;
    private GameObject[] pegs_left;

    private Camera cam;
    private bool zoom = false;
    public float zoom_speed = 0.1F;
    public float zoom_step = 0.1F;
    public float norm_zoom = 5F;
    public float max_zoom = 1F;

    private void Start()
    {
        launcher = GameObject.FindGameObjectWithTag("launcher");
        pegs_left = GameObject.FindGameObjectsWithTag("peg");
        cam = Camera.main;

        pegs_left = GameObject.FindGameObjectsWithTag("peg");
        if (pegs_left.Length == 1)
        {
            //if ball != null
            pegs_left[0].AddComponent<CircleCollider2D>();
            pegs_left[0].GetComponent<CircleCollider2D>().isTrigger = true;
            pegs_left[0].GetComponent<CircleCollider2D>().radius = 3.0F;

            pegs_left[0].AddComponent<slow_motion>();
            pegs_left[0].AddComponent<camera_zoom>();
        }
    }

        // Update is called once per frame
    void Update () {
        if (zoom)
        {
            if (cam.GetComponent<Camera>().orthographicSize >= max_zoom)
            {
                cam.GetComponent<Camera>().orthographicSize -= zoom_step;
            }
        }
        else
        {
            if (cam.GetComponent<Camera>().orthographicSize <= norm_zoom)
            {
                cam.GetComponent<Camera>().orthographicSize += zoom_step;
            }
        }

    }

    public void BallCollision(Collision2D collision)
    {
        Debug.Log("Hit: " + collision.gameObject);
        collision.gameObject.tag = "hit";
        collision.gameObject.GetComponent<SpriteRenderer>().color = Color.red;


        pegs_left = GameObject.FindGameObjectsWithTag("peg");
        if (pegs_left.Length == 1)
        {
            //if ball != null
            pegs_left[0].AddComponent<CircleCollider2D>();
            pegs_left[0].GetComponent<CircleCollider2D>().isTrigger = true;
            pegs_left[0].GetComponent<CircleCollider2D>().radius = 3.0F;

            pegs_left[0].AddComponent<slow_motion>();
            pegs_left[0].AddComponent<camera_zoom>();

        }
    }

    public void ResetBall(Collider2D ball)
    {
        Destroy(ball.gameObject);
        balls --;
        Debug.Log(balls + " lives left");
        Debug.Log(pegs_left.Length + " pegs left");

        if (balls > 0 && pegs_left.Length > 0)
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

    public void EnterSlowMotion()
    {

    }
    public void ExitSlowMotion()
    {

    }
    public void EnterCameraZoom()
    {
        zoom = true;
    }


    public void ExitCameraZoom()
    {
        zoom = false;
    }
}
