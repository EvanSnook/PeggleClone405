using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class last_peg_audio : MonoBehaviour {

    private GameObject game_manager;

    private void Start()
    {
        game_manager = GameObject.Find("GameManager");
    }

    void OnTriggerEnter2D(Collider2D ball)
    {
        Debug.Log("Activate anticipation music");

        this.gameObject.GetComponent<AudioSource>().Play();

        game_manager.SendMessage("PauseBackgroundMusic");
    }
    void OnTriggerExit2D(Collider2D ball)
    {
        Debug.Log("Deactivate anticipation music");

        this.gameObject.GetComponent<AudioSource>().Stop();

        game_manager.SendMessage("ResumeBackgroundMusic");
    }
}
