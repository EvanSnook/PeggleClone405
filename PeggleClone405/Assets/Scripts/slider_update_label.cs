using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class slider_update_label : MonoBehaviour {

    public GameObject input_slider;
    private string original_text;
	// Use this for initialization
	void Start () {
        original_text = this.gameObject.GetComponent<Text>().text;

    }
	
	// Update is called once per frame
	void Update () {
        this.gameObject.GetComponent<Text>().text = original_text + input_slider.GetComponent<Slider>().value.ToString();
	}
}
