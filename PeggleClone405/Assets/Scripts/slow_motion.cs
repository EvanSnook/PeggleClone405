using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//sends messages to the game_manager when a trigger is entered and exited
public class slow_motion : MonoBehaviour {

    private GameObject game_manager;

    private void Start()
    {
        game_manager = GameObject.Find("GameManager");
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
