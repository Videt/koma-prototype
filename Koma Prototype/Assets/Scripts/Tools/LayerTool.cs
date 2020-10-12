using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]
public class LayerTool : MonoBehaviour
{
    public SpriteRenderer sp;

    void Start()
    {
        
    }

    void Update()
    {
        sp.sortingOrder = Mathf.FloorToInt(transform.position.z);
    }
    private void OnEnable()
    {
        sp = GetComponent<SpriteRenderer>();
    }
}
