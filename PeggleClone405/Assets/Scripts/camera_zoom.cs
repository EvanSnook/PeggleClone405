using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//sends a message to the game_manager when the triger is entered or exited to zoom or unzoom the camera
public class camera_zoom : MonoBehaviour {

    private GameObject game_manager;

    private void Start()
    {
        game_manager = GameObject.Find("GameManager");
    }

    void OnTriggerEnter2D(Collider2D ball)
    {
        Debug.Log("Activate Camera Zoom");
        game_manager.SendMessage("EnterCameraZoom");

    }
    void OnTriggerExit2D(Collider2D ball)
    {
        Debug.Log("Deactivate Camera Zoom");
        game_manager.SendMessage("ExitCameraZoom");

    }
}
