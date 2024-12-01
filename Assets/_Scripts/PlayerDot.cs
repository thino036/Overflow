using UnityEngine;

public class PlayerDot : MonoBehaviour
{
    public RectTransform playerDot;
    public Transform playerObject;
    public RectTransform sliderTransform;
    public float maxHeight = 100f;
    public float minHeight = -30f;

    void Update()
    {
        if (playerDot != null && playerObject != null && sliderTransform != null)
        {
            float normalizedY = (playerObject.position.y - minHeight) / (maxHeight - minHeight);

            float sliderHeight = sliderTransform.rect.height;

            // Move the PlayerDot proportionally on the slider
            playerDot.anchoredPosition = new Vector2(
                playerDot.anchoredPosition.x,
                Mathf.Clamp(normalizedY * sliderHeight, 0, sliderHeight) * 2
            );
        }
    }
}
