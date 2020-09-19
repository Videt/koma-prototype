using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camer_Zoom : MonoBehaviour
{
    private Camera camera;
    private float speed;
    public float smooth;//величина, на которую меняется каждый кадр
    private bool isZoomed;//переключатель
    public float zoomMax;//величина увеличения
    public void OnEnable()
    {
        camera = GetComponent<Camera>();
    }


    public void Zoom(float _zoom) // то к чему стремится
    {
        isZoomed = true;
        zoomMax = _zoom;
    }
    public void FixedUpdate()
    {
        if (isZoomed)
        {
            camera.orthographicSize = Mathf.SmoothDamp(camera.orthographicSize, zoomMax, ref speed, smooth);
        }
        if (System.Math.Round(camera.orthographicSize,2) == zoomMax)
        {
            isZoomed = false;
        }
    }
}
