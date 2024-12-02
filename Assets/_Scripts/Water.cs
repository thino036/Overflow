using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Water : MonoBehaviour
{
    public GameObject waterPrefab;
    public Slider waterBar;
    public float speed = 1f;
    public float maxWaterHeight = 200f;
    public float levelHeight = 50f;
    public float sliderSpeed = 0.8f;

    private float currentWaterLevel = 0f;
    private float displayedWaterLevel = 0f;

    void Start()
    {
        // Dynamically match the water bar to the level height
        //maxWaterHeight = levelHeight;

        // Slider's starting value and range matches the water level
        if (waterBar != null)
        {
            waterBar.maxValue = maxWaterHeight;
            waterBar.value = displayedWaterLevel;
        }
    }

    void Update()
    {
        // Increase water level
        if (currentWaterLevel < maxWaterHeight)
        {
            currentWaterLevel += speed * Time.deltaTime;

            // Update the water's scale to match the full level height
            Vector3 scale = transform.localScale;
            scale.y = (currentWaterLevel / maxWaterHeight) * levelHeight;
            transform.localScale = scale;

            // Update the Slider value
            if (waterBar != null)
            {
                displayedWaterLevel = Mathf.Lerp(displayedWaterLevel, currentWaterLevel, sliderSpeed * Time.deltaTime);
                waterBar.value = displayedWaterLevel;
            }
        }
    }
}
