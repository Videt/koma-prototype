using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicTrees : MonoBehaviour
{
    float levelTimer;
    float rotationObj;
    public float Speed;
    public float Range;
    public bool Randomize;
    public bool Inverse;

    void Start()
    {

    }


    void Update()
    {
        int axis = 1;

        if (Randomize)
        {
            InRandom();
        }
        if (Inverse)
        {
            axis = -1;
        }
        else
        {
            axis = 1;
        }
            
        levelTimer += Time.deltaTime;
        rotationObj = Mathf.Sin(levelTimer * Speed * axis);
        transform.rotation = Quaternion.Euler(0, 0, Range * rotationObj);
    }
    void InRandom()
    {
        Randomize = false;
        Speed = Random.Range(1,2);
        Range = Random.Range(.5f,2);

    }
}
