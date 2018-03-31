using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;




public class game_manager : MonoBehaviour {

    public int balls = 3; //number of balls allowed in the level

    private GameObject ball_counter; //UI element that displays how many balls are left
    private GameObject launcher; //game object that shoots the ball
    private GameObject[] hit_pegs; //list of pegs that have been hit by the ball
    public GameObject explosion_animation; //prefab of an explosion sprite
    private GameObject[] pegs_left; // list of pegs that have not been hit
    private GameObject level_complete_panel; //UI panel at the completion of a level
    private GameObject level_failed_panel; //UI panel at the failure of a level
    private GameObject main_menu_button; // UI button that brings the user back to the main menu
    private Camera cam; // the main camera

    public float slow_mo_scale = 0.1f; // control time speed in slow motion mode
    public float explosion_size = 1.0f; // control the size of the explosion_animation
    private bool zoom = false; //is the camera zoomed in or not
    private bool endgame = false; //has the level ended or not
    public float zoom_speed = 0.1F; // how fast the camera should zoom
    public float zoom_step = 0.1F; // the steps that the camera zooms in with
    public float norm_zoom = 5F; // the normal position of the camera
    public float max_zoom = 1F; // the furthers possible zoom of the camera

    private GameObject settings_object; //doesnt destroy on load from the main menu to bring some settings over
    
    private void Start()
    {
        //set the time to normal speed
        Time.timeScale = 1.0F;
        //find the launcher, pegs_left, camera, and UI elements
        launcher = GameObject.Find("Launcher");
        pegs_left = GameObject.FindGameObjectsWithTag("peg");
        cam = Camera.main;
        level_complete_panel = GameObject.Find("LevelCompletePanel");
        level_failed_panel = GameObject.Find("LevelFailedPanel");
        main_menu_button = GameObject.Find("InGameMainMenuButton");
        ball_counter = GameObject.Find("BallCount");

        //set the bounter UI element text to be the balls left of the current level
        ball_counter.GetComponent<Text>().text = "Balls Left: " + balls;

        //set the end game UI to false until the game ends
        level_complete_panel.SetActive(false);
        level_failed_panel.SetActive(false);

        //looks for game settings to adjust the way that the level ends (camera zoom, slow mo, and explosion size)
        if (GameObject.Find("settings") != null) {
            Debug.Log("updating variables from settings");
            settings_object = GameObject.Find("settings");
            max_zoom = settings_object.GetComponent<scaling_settings>().max_zoom;
            slow_mo_scale = settings_object.GetComponent<scaling_settings>().slow_mo_scale;
            explosion_size = settings_object.GetComponent<scaling_settings>().explosion_size;
        }
        else
        {
            Debug.Log("settings not found");
        }

        pegs_left = GameObject.FindGameObjectsWithTag("peg");

        //if there is one peg left then add a trigger to it to trigger the end level events
        if (pegs_left.Length == 1)
        {
            Debug.Log("Adding trigger components to last peg");

            pegs_left[0].AddComponent<CircleCollider2D>();
            pegs_left[0].GetComponent<CircleCollider2D>().isTrigger = true;
            pegs_left[0].GetComponent<CircleCollider2D>().radius = 3.0F;

            //also the scripts to trigger when the ball enters thetrigger zone
            pegs_left[0].AddComponent<slow_motion>();
            pegs_left[0].AddComponent<camera_zoom>();
        }
    }

    // Update is called once per frame
    void Update () {
        //while the zooming mode is activated the camera will zoom in towards the last peg up to a max_zoom by a zoom_step
        if (zoom)
        {
            if (cam.GetComponent<Camera>().orthographicSize >= max_zoom)
            {
                cam.GetComponent<Camera>().orthographicSize -= zoom_step;
            }
            cam.transform.position = Vector3.MoveTowards(cam.transform.position, new Vector3(pegs_left[0].transform.position.x, pegs_left[0].transform.position.y, -1), zoom_step);
            
        }
        //while the zooming mode is deactivated the camera will zoom out towards the origin up to a normal_zoom by a zoom_step
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

        //if the object has been hit once already, then destroy the blocks early to prevent the ball from bouncing between 2 things for hours
        if (collision.gameObject.tag == "hit")
        {
            Debug.Log("peg hit twice - destroying pegs early");
            StartCoroutine("DestroyBlocksEarly");
        }

        //mar the hit peg as hit and change its cour for visibility
        if (collision.gameObject.tag == "peg")
        {
            collision.gameObject.tag = "hit";
            collision.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        }

        //find pegs left and if there is one peg left then add a trigger to it to trigger the end level events
        pegs_left = GameObject.FindGameObjectsWithTag("peg");
        if (pegs_left.Length == 1)
        {
            Debug.Log("adding trigger to the last peg");
            pegs_left[0].AddComponent<CircleCollider2D>();
            pegs_left[0].GetComponent<CircleCollider2D>().isTrigger = true;
            pegs_left[0].GetComponent<CircleCollider2D>().radius = 3.0F;

            pegs_left[0].AddComponent<slow_motion>();
            pegs_left[0].AddComponent<camera_zoom>();

        }

        //if there are 0 pegs left, then ends the level and destroy the pegs
        if(pegs_left.Length == 0)
        {
            Debug.Log("ending level");
            endgame = true;
            ExplodeBlocks();
        }
    }

    //wait 1 second then destroy the blocks
    IEnumerator DestroyBlocksEarly()
    {
        yield return new WaitForSeconds(1.0F);
        ExplodeBlocks();
    }

    //called when the ball exits the boundaries
    public void ResetBall(Collider2D ball)
    {
        //destroy the ball and update the counter
        Destroy(ball.gameObject);
        balls --;
        ball_counter.GetComponent<Text>().text = "Balls Left: " + balls;

        Debug.Log(balls + " lives left");
        Debug.Log(pegs_left.Length + " pegs left");

        //check if the level is over and allow the launcher to fire again if its not
        if (balls > 0 && pegs_left.Length > 0)
        {
            launcher.SendMessage("resetFire");
        }
        else
        {
            Debug.Log("Game Over");
            ShowLevelFailedPanel();
        }
        
    }
    
    public void ExplodeBlocks()
    {
        //find all the pegs that have been hit
        hit_pegs = GameObject.FindGameObjectsWithTag("hit");
        foreach (GameObject block in hit_pegs)
        {
            //save the position
            Vector3 block_position = new Vector3(block.transform.position.x, block.transform.position.y, block.transform.position.z);
            Debug.Log("destroying " + block.gameObject);
            //destroy the peg
            Destroy(block.gameObject);
            //create the explosion where the peg was
            GameObject xplosion = Instantiate(explosion_animation ,block_position, new Quaternion(0,0,0,0));
            //scale the explosion
            xplosion.transform.localScale = new Vector3(explosion_size, explosion_size,1F);
            //destroy the explosion after a period of time
            StartCoroutine(WaitForAnimation(xplosion));

        }
    }

    IEnumerator WaitForAnimation(GameObject xplosion)
    {
        //time that the coroutine ewaits depends if the game is in slo_mo or not
        if (endgame)
        {
            yield return new WaitForSeconds(0.5F);
        }
        else
        {
            yield return new WaitForSeconds(1.0F);
        }

        Destroy(xplosion);

        //show the end level menu
        if (endgame)
        {
            ShowLevelCompletePanel();
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
        if (!endgame)
        {
            zoom = false;
        }
    }

    public void ShowLevelCompletePanel()
    {
        level_complete_panel.SetActive(true);
        main_menu_button.SetActive(false);
        
    }

    public void ShowLevelFailedPanel()
    {
        level_failed_panel.SetActive(true);
        main_menu_button.SetActive(false);

    }
}
