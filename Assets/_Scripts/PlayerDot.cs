using UnityEngine;

public class PlayerDot : MonoBehaviour
{
    public RectTransform playerDot; // The PlayerDot UI element
    public Transform playerObject; // The player GameObject
    public RectTransform sliderTransform; // The slider's RectTransform
    public float maxHeight = 10f; // The maximum height the player can reach
    public float scalingFactor = 0.2f; // Controls how much the dot moves along the slider

    void Update()
    {
        if (playerDot != null && playerObject != null && sliderTransform != null)
        {
            // Calculate the player's Y position as a percentage of maxHeight, then scale it down
            float playerYPercent = Mathf.Clamp01((playerObject.position.y / maxHeight) * scalingFactor);

            // Move the PlayerDot along the slider based on the scaled percentage
            float sliderHeight = sliderTransform.rect.height; // Get the slider's height
            playerDot.anchoredPosition = new Vector2(
                playerDot.anchoredPosition.x, // Keep the X position the same
                playerYPercent * sliderHeight // Adjust the Y position based on scaled percentage
            );
        }
    }
}
