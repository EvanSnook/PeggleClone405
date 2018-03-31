using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//loads a scene by the index that it is un the build settings
public class load_scene_onclick : MonoBehaviour {

    //scene index is a variable inserted when the UI element calls the function
    public void LoadByindex(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
}
