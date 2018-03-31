using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class launcher_shoot : MonoBehaviour {

    public float projectile_speed = 500; // Movement Speed of the projectile.
    public GameObject projectile_prefab; // The Projectile that will be fired.
    private GameObject clone; // The fired Projectile.


    private GameObject end_game_panel; //UI panel shown at end of game

    private bool can_fire; // This keeps track of if you can fire or not.

    private Vector3 mouse_position; //position of your mouse on the screen
    private Quaternion angle_to_mouse; // angle to your mouse on the screen

    void Start()
    {
        Debug.Log("Ready to fire");
        can_fire = true;
    }


    void FixedUpdate()
    {
        if (Input.GetAxisRaw("Fire1") > 0.1)
        { 
            // This get's the input for Fire1 and calls fire function if UI elements are not open
            getMousePosition();

            if (GameObject.FindGameObjectWithTag("LevelCompleteMenu") == null)
            {
                FireProjectileAtMouse();
            }
        }
    }


    // This Finds the Mouse and Fires a Projectile in the Mouses Direction.
    private void FireProjectileAtMouse()
    {
        if (can_fire)
        {
            Debug.Log("Ball fired");
            can_fire = false;
            Debug.Log("Fire on cooldown");

            //creates the projectil and then adds force to it
            clone = Instantiate(projectile_prefab, transform.position, angle_to_mouse) as GameObject;
            clone.GetComponent<Rigidbody2D>().AddForce(clone.transform.up * -projectile_speed);
            
        }

    }

    void getMousePosition()
    {
        // Get the Mouse Position on the screen
        mouse_position = Input.mousePosition;

        // subtract the cameras z axisfrom the mouse position to put the vecctor on the same plane as the game
        mouse_position.z = transform.position.z - Camera.main.transform.position.z;

        //change the cooridinate type from screen position of the computer to the world position within the game
        mouse_position = Camera.main.ScreenToWorldPoint(mouse_position);


        //get angle to the mouse from the gameObject which is instantiated pointing down
        angle_to_mouse = Quaternion.FromToRotation(Vector3.down, mouse_position - transform.position);
    }

    public void resetFire()
    {
        Debug.Log("Ready to fire");
        //sets the variable to true so that the launcher is ready to fire again
        can_fire = true;
    }
}
