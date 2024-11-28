using UnityEngine;
using UnityEngine.UI;

public class Oxygen : MonoBehaviour
{
    public Slider oxygenBar; // Reference to the UI Slider for oxygen
    public float maxOxygen = 100f; // Maximum oxygen level
    public float depletionRate = 0.01f; // Oxygen depletion rate per second
    private float currentOxygen; // Current oxygen level

    void Start()
    {
        // Initialize the oxygen bar
        currentOxygen = maxOxygen;

        if (oxygenBar != null)
        {
            oxygenBar.maxValue = maxOxygen;
            oxygenBar.value = currentOxygen;
        }
    }

    void Update()
    {
        if (oxygenBar != null && currentOxygen > 0)
        {
            // Deplete oxygen over time
            currentOxygen -= depletionRate * Time.deltaTime;

            // Update the oxygen bar value
            oxygenBar.value = currentOxygen;
        }

        // Prevent oxygen from dropping below 0
        currentOxygen = Mathf.Max(0, currentOxygen);
    }

    // Function to replenish oxygen (call this from other scripts)
    public void ReplenishOxygen(float amount)
    {
        currentOxygen = Mathf.Min(currentOxygen + amount, maxOxygen); // Cap at maxOxygen
        if (oxygenBar != null)
        {
            oxygenBar.value = currentOxygen;
        }
    }
}
