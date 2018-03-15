using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slow_motion : MonoBehaviour {

    private GameObject game_manager;

    private void Start()
    {
        game_manager = GameObject.FindGameObjectWithTag("game_manager");
    }

    void OnTriggerEnter2D(Collider2D ball)
    {
        Debug.Log("Activate Slow motion");
        game_manager.SendMessage("EnterSlowMotion");

    }
    void OnTriggerExit2D(Collider2D ball)
    {
        Debug.Log("Deactivate Slow motion");
        game_manager.SendMessage("ExitSlowMotion");

    }

}
