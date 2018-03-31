using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//script for quit button
public class quit_onclick : MonoBehaviour {

    public void Quit()
    {
        //quits to the editor if in the editor and quits the app if in an app
        #if UNITY_EDITOR
            Debug.Log("Quit through editor");
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
