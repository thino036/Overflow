using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Import to use UI elements

public class Water : MonoBehaviour
{
    public GameObject waterPrefab; // Water object
    public Slider waterBar;        // Slider to represent water level
    public float speed = 1f;       // Speed of water rising
    public float maxWaterHeight = 10f; // Maximum height of the water object

    private float currentWaterLevel = 0f; // Track the current water level

    void Start()
    {
        // Initialize the water bar's max value
        if (waterBar != null)
        {
            waterBar.maxValue = maxWaterHeight; // Match max water height
            waterBar.value = currentWaterLevel;
        }
    }

    void Update()
    {
        // Increase water level
        if (currentWaterLevel < maxWaterHeight)
        {
            currentWaterLevel += speed * Time.deltaTime; // Increase water level over time

            // Update the water's scale
            Vector3 scale = transform.localScale;
            scale.y = currentWaterLevel; // Set the water's height directly
            transform.localScale = scale;

            // Update the Slider value
            if (waterBar != null)
            {
                waterBar.value = currentWaterLevel;
            }
        }
    }
}
