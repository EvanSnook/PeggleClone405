using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followMouse : MonoBehaviour {

    private Quaternion angleToMouse;
    private Vector3 mousePosition;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        getMousePosition();

        //set the rotation to that angle to mouse
        this.transform.rotation = angleToMouse;

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

}
