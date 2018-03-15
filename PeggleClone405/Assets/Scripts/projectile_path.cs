using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile_path : MonoBehaviour {

    public GameObject TrajectoryPointPrefeb;

    private bool showPath = true;
    private float projectileSpeed = 1;
    private int numberOfPoints = 200;
    private List<GameObject> points = new List<GameObject>();

    //---------------------------------------

    void Start()
    {

        //   TrajectoryPoints are instatiated
        for (int i = 0; i < numberOfPoints; i++)
        {
            GameObject dot = (GameObject)Instantiate(TrajectoryPointPrefeb);
            dot.GetComponent<SpriteRenderer>().enabled = false;
            points.Insert(i, dot);
        }
    }

    //---------------------------------------

    void Update()
    {
        if (showPath) {

            Vector3 vel = GetForceFrom(this.transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition));
            float angle = Mathf.Atan2(vel.y, vel.x) * Mathf.Rad2Deg;
            transform.eulerAngles = new Vector3(0, 0, angle);
            setTrajectoryPoints(transform.position, vel);
        }
    }


    //---------------------------------------    
    // Following method returns force by calculating distance between given two points
    //---------------------------------------    
    private Vector2 GetForceFrom(Vector3 fromPos, Vector3 toPos)
    {
        return (new Vector2(toPos.x, toPos.y) - new Vector2(fromPos.x, fromPos.y)) * projectileSpeed;
    }
    //---------------------------------------    
    // Following method displays projectile trajectory path. It takes two arguments, start position of object(ball) and initial velocity of object(ball).
    //---------------------------------------    
    void setTrajectoryPoints(Vector3 pStartPosition, Vector3 pVelocity)
    {
        float velocity = Mathf.Sqrt((pVelocity.x * pVelocity.x) + (pVelocity.y * pVelocity.y));
        float angle = Mathf.Rad2Deg * (Mathf.Atan2(pVelocity.y, pVelocity.x));
        float fTime = 0;

        fTime += 0.1f;
        for (int i = 0; i < numberOfPoints; i++)
        {
            float dx = velocity * fTime * Mathf.Cos(angle * Mathf.Deg2Rad);
            float dy = velocity * fTime * Mathf.Sin(angle * Mathf.Deg2Rad) - (Physics2D.gravity.magnitude * fTime * fTime / 2.0f);
            Vector3 pos = new Vector3(pStartPosition.x + dx, pStartPosition.y + dy, 2);
            points[i].transform.position = pos;
            points[i].GetComponent<SpriteRenderer>().enabled = true;
            points[i].transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2(pVelocity.y - (Physics.gravity.magnitude) * fTime, pVelocity.x) * Mathf.Rad2Deg);
            fTime += 0.1f;
        }
    }
}
