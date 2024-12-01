using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [Tooltip("Reference to the player's health script.")]
    public PlayerCube player;
    [Tooltip("Slider component for the health bar.")]
    public Slider healthSlider;

    void Start()
    {
        if (player == null)
        {
            Debug.LogError("PlayerCube reference is missing!");
            return;
        }

        if (healthSlider == null)
        {
            Debug.LogError("Health Slider reference is missing!");
            return;
        }

        // Initialize the slider with max health
        healthSlider.maxValue = player.playerHealth;
        healthSlider.value = player.playerHealth;
    }

    void Update()
    {
        // Update the slider value based on the player's current health
        if (player != null && healthSlider != null)
        {
            healthSlider.value = player.playerHealth;
        }
    }
}
