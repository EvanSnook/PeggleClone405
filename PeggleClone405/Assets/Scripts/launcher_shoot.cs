using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class launcher_shoot : MonoBehaviour {

    public float projectileSpeed = 500; // Movement Speed of the projectile.
    public GameObject projectilePrefab; // The Projectile that will be fired.
    private GameObject clone; // The fired Projectile.

    public GameObject pointPrefab;
    private List<GameObject> points = new List<GameObject>();
    public int numPoints = 20;
    float fTime = 0;

    private bool canFire; // This keeps track of if you can fire or not.
    private Vector3 mousePosition;
    private Quaternion angleToMouse;

    void Start()
    {
        Debug.Log("Ready to fire");
        canFire = true;

        //points are instatiated
        for (int i = 0; i < numPoints; i++)
        {
            GameObject dot = Instantiate(pointPrefab);
            dot.GetComponent<SpriteRenderer>().enabled = false;
            points.Add(dot);
        }
    }

    void Update()
    {
        fTime = 0.1f;

        //points are given a position
        for (int i = 0; i < numPoints; i++)
        {
            float dx =  fTime;
            float dy =  fTime - (Physics2D.gravity.magnitude * fTime);
            Vector3 pos = new Vector3(this.gameObject.transform.position.x + dx, this.gameObject.transform.position.y + dy, 0);

            points[i].GetComponent<Transform>().position = pos;

            points[i].GetComponent<SpriteRenderer>().enabled = true;

            fTime += 0.1f;
        }
    }

    void FixedUpdate()
    {
        if (Input.GetAxisRaw("Fire1") > 0.1)
        { // This get's the input for Fire1 and sends message to fire if pushed.
            
            FireProjectileAtMouse();
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

            getMousePosition();
            
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
