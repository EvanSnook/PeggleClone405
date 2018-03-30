using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;




public class game_manager : MonoBehaviour {

    public int balls = 3;
    private GameObject ball_counter;

    public float slow_mo_scale = 0.1f;
    public float explosion_size = 1.0f;

    private GameObject launcher;
    private GameObject[] hit_blocks;
    public GameObject explosion_animation;
    private Vector3 block_position;
    private GameObject[] pegs_left;

    private GameObject end_game_panel;
    private GameObject ingame_settings_panel;
    private GameObject settings_button;

    private Camera cam;
    private bool zoom = false;
    private bool endgame = false;
    public float zoom_speed = 0.1F;
    public float zoom_step = 0.1F;
    public float norm_zoom = 5F;
    public float max_zoom = 1F;

    private GameObject settings_object;
    
    private void Start()
    {
        Time.timeScale = 1.0F;
        launcher = GameObject.FindGameObjectWithTag("launcher");
        pegs_left = GameObject.FindGameObjectsWithTag("peg");
        cam = Camera.main;

        end_game_panel = GameObject.FindGameObjectWithTag("LevelCompleteMenu");
        ingame_settings_panel = GameObject.FindGameObjectWithTag("InGameSettingsMenu");
        settings_button = GameObject.FindGameObjectWithTag("SettingsButton");
        ball_counter = GameObject.FindGameObjectWithTag("ballCounter");
        ball_counter.GetComponent<Text>().text = "Balls Left: " + balls;

        end_game_panel.SetActive(false);
        ingame_settings_panel.SetActive(false);
        if (GameObject.Find("settings") != null) {
            settings_object = GameObject.Find("settings");
            max_zoom = settings_object.GetComponent<scaling_settings>().max_zoom;
            slow_mo_scale = settings_object.GetComponent<scaling_settings>().slow_mo_scale;
            explosion_size = settings_object.GetComponent<scaling_settings>().explosion_size;
        }
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
            cam.transform.position = Vector3.MoveTowards(cam.transform.position, new Vector3(pegs_left[0].transform.position.x, pegs_left[0].transform.position.y, -1), zoom_step);
            
        }
        else 
        {
            if (cam.GetComponent<Camera>().orthographicSize <= norm_zoom)
            {
                cam.GetComponent<Camera>().orthographicSize += zoom_step;
            }
            cam.transform.position = Vector3.MoveTowards(cam.transform.position, new Vector3(0F,0F,-1F), zoom_step);
           
        }

    }

    public void BallCollision(Collision2D collision)
    {
        Debug.Log("Hit: " + collision.gameObject);

        if (collision.gameObject.tag == "hit")
        {
            StartCoroutine("DestroyBlocksEarly");
        }

        if (collision.gameObject.tag == "peg")
        {
            collision.gameObject.tag = "hit";
            collision.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        }

            pegs_left = GameObject.FindGameObjectsWithTag("peg");
        if (pegs_left.Length == 1)
        {
            pegs_left[0].AddComponent<CircleCollider2D>();
            pegs_left[0].GetComponent<CircleCollider2D>().isTrigger = true;
            pegs_left[0].GetComponent<CircleCollider2D>().radius = 3.0F;

            pegs_left[0].AddComponent<slow_motion>();
            pegs_left[0].AddComponent<camera_zoom>();

        }
        if(pegs_left.Length == 0)
        {
            endgame = true;
            ExplodeBlocks();
        }
    }

    IEnumerator DestroyBlocksEarly()
    {
        yield return new WaitForSeconds(1.0F);
        
        ExplodeBlocks();
    }

    public void ResetBall(Collider2D ball)
    {
        Destroy(ball.gameObject);
        balls --;
        ball_counter.GetComponent<Text>().text = "Balls Left: " + balls;
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
            
            GameObject xplosion = Instantiate(explosion_animation ,block_position, new Quaternion(0,0,0,0));

            xplosion.transform.localScale = new Vector3(explosion_size, explosion_size,1F);

            StartCoroutine(WaitForAnimation(xplosion));

        }
    }

    IEnumerator WaitForAnimation(GameObject xplosion)
    {
        //time it takes explosion animation to complete
        if (endgame)
        {
            yield return new WaitForSeconds(0.5F);
        }
        else
        {
            yield return new WaitForSeconds(1.0F);
        }

        Destroy(xplosion);

        if (endgame)
        {
            ShowLevelMenu();
        }
    }

    public void EnterSlowMotion()
    {
        Time.timeScale = slow_mo_scale;
    }

    public void ExitSlowMotion()
    {
        if (!endgame)
        {
            Time.timeScale = 1.0F;
        }
    }

    public void EnterCameraZoom()
    {
        zoom = true;
    }


    public void ExitCameraZoom()
    {
        zoom = false;
    }

    public void ShowLevelMenu()
    {
        end_game_panel.SetActive(true);
        settings_button.SetActive(false);
        ingame_settings_panel.SetActive(false);
        
    }
}
