using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class launcher_shoot : MonoBehaviour {

    public float projectileSpeed = 500; // Movement Speed of the projectile.
    public GameObject projectilePrefab; // The Projectile that will be fired.
    private GameObject clone; // The fired Projectile.


    private GameObject end_game_panel;
    private GameObject ingame_settings_panel;
    private GameObject settings_button;

    private bool canFire; // This keeps track of if you can fire or not.
    private Vector3 mousePosition;
    private Quaternion angleToMouse;

    void Start()
    {
        settings_button = GameObject.FindGameObjectWithTag("SettingsButton");

        Debug.Log("Ready to fire");
        canFire = true;
    }


    void FixedUpdate()
    {
        if (Input.GetAxisRaw("Fire1") > 0.1)
        { // This get's the input for Fire1 and sends message to fire if pushed.

            getMousePosition();
            if (GameObject.FindGameObjectWithTag("LevelCompleteMenu") == null && GameObject.FindGameObjectWithTag("InGameSettingsMenu") == null
                && settings_button.GetComponent<BoxCollider2D>().bounds.Contains(mousePosition))
            {
             FireProjectileAtMouse();
            }
        }
    }


    // This Finds the Mouse and Fires a Projectile in the Mouses Direction.
    private void FireProjectileAtMouse()
    {
        if (canFire)
        {

            Debug.Log("Ball fired");
            Debug.Log("Fire on cooldown");
            canFire = false;

            clone = Instantiate(projectilePrefab, transform.position, angleToMouse) as GameObject;
            clone.GetComponent<Rigidbody2D>().AddForce(clone.transform.up * -projectileSpeed); // Launch Projectile forward to Mouse.
            
        }

    }

    void getMousePosition()
    {
        // Get the Mouse Position on the screen
        mousePosition = Input.mousePosition;

        // subtract the cameras z axisfrom the mouse position to put the vecctor on the same plane as the game
        mousePosition.z = transform.position.z - Camera.main.transform.position.z;

        //change the cooridinate type from screen position of the computer to the world position within the game
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);


        //get angle to the mouse from the gameObject which is instantiated pointing down
        angleToMouse = Quaternion.FromToRotation(Vector3.down, mousePosition - transform.position);
    }

    public void resetFire()
    {
        Debug.Log("Ready to fire");
        canFire = true;
    }
}
