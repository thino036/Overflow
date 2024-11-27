using UnityEngine;
using UnityEngine.UI;

public class PlayerTracker : MonoBehaviour
{
    public GameObject playerCube; // Reference to the PlayerCube GameObject
    public Text playerYLevelText; // Reference to the Text UI element for displaying Y-level

    private float baseYLevel = 0f; // Start Y-level at 0 explicitly

    void Start()
    {
        // Set the base Y-level to 0
        baseYLevel = 0f;
    }

    void Update()
    {
        if (playerCube != null && playerYLevelText != null)
        {
            // Calculate adjusted Y-level
            float adjustedYLevel = playerCube.transform.position.y - baseYLevel;

            // Clamp the Y-level to always stay at or above 0
            adjustedYLevel = Mathf.Max(0, adjustedYLevel);

            // Update the Text UI element
            playerYLevelText.text = adjustedYLevel.ToString("F2") + "m";
        }
    }
}
