using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    public GameObject waterPrefab;

    public float speed = 1f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Increase water level
        Vector3 scale = transform.localScale;
        scale.y += speed * Time.deltaTime;
        transform.localScale = scale;
    }
}
