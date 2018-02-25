using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class load_scene_onclick : MonoBehaviour {

    public void LoadByindex(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
}
