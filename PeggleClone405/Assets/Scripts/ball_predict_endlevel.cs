using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ball_predict_endlevel : MonoBehaviour {

    public float step = 0.01F;
    public float distance = 2;

    private GameObject[] blocks;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
        blocks = GameObject.FindGameObjectsWithTag("peg");

        for (float i = 0F; i < distance; i += step)
        {

            Vector3 futurePosition = new Vector3((float)(this.gameObject.GetComponent<Transform>().position.x * this.gameObject.GetComponent<Rigidbody2D>().velocity.x),
                                                  (float)(this.gameObject.GetComponent<Transform>().position.y * this.gameObject.GetComponent<Rigidbody2D>().velocity.y),
                                                  1.0F);

            //iterate through pegs and =check if futurePOsition +- radius is in any of them
            for (int j = 0; j < blocks.Length ; j++) {
                if (blocks[j].GetComponent<PolygonCollider2D>().bounds.Contains(futurePosition))
                {
                    Debug.Log("IT WORKED");
                }
            }
        }
	}
}
