using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class LampLight : MonoBehaviour
{
    private Light2D lightLamp;
    public bool isLighting = true;
    public float smooth_swith;
    [Range(0,5)]
    public float intensity_light;



    void Start()
    {
        lightLamp = gameObject.GetComponent<Light2D>();
        intensity_light = lightLamp.intensity;
    }
    private void Update()
    {
        if (isLighting)
        {       
            if (lightLamp.intensity < intensity_light)
            {
                lightLamp.intensity += smooth_swith;
            }
        }
        else
        {
            if (lightLamp.intensity > 0)
            {
                lightLamp.intensity -= smooth_swith;
            }
        }
    }
    public void Swith()
    {
        isLighting = !isLighting;
    }

   
}
