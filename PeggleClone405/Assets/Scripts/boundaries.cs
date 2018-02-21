using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boundaries : MonoBehaviour {

    public GameObject launcher;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}



    void OnTriggerExit2D(Collider2D ball)
    {
        Debug.Log("ded");
        Destroy(ball.gameObject);
        launcher.SendMessage("resetFire");
    }
}
