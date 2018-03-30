using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class settings : MonoBehaviour {

    public float slow_mo_scale = 0.1f;
    public float explosion_size = 1.0f;
    public float zoom_speed = 0.1F;
    public float zoom_step = 0.1F;
    public float norm_zoom = 5F;
    public float max_zoom = 1F;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void AdjustSlowMo(float new_speed)
    {
        slow_mo_scale = new_speed;
    }

    public void Adjustexplosion(float new_size)
    {
        explosion_size = new_size;
    }

    public void AdjustZoom(float new_zoom)
    {
        max_zoom = new_zoom;
    }
}
