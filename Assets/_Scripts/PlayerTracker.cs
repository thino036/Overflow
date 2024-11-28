using UnityEngine;
using UnityEngine.UI;

public class PlayerTracker : MonoBehaviour
{
    public GameObject playerCube;
    public Text playerYLevelText;

    private float baseYLevel = 0f;

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

            // Make sure y-level stays above 0
            adjustedYLevel = Mathf.Max(0, adjustedYLevel);

            // Text UI element
            playerYLevelText.text = adjustedYLevel.ToString("F2") + "m";
        }
    }
}
