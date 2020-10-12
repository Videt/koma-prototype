using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camer_Zoom : MonoBehaviour
{
    public float smoothTime;
    private Camera camera;
    public float speed; //забей, не трошь
    private bool isZoomed;
    public void OnEnable()
    {
        camera = GetComponent<Camera>();
    }


    public void Zoom(float zoomDump) // то к чему стремится
    {
        camera.orthographicSize = Mathf.SmoothDamp(camera.orthographicSize, zoomDump, ref speed, smoothTime);
        
        
    }
    public void FixedUpdate()
    {
        Zoom(20);
    }
}
