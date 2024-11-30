using UnityEngine;
using UnityEngine.UI;

public class PlayerTracker : MonoBehaviour
{
    public GameObject playerCube;
    public Text playerYLevelText;

    void Update()
    {
        if (playerCube != null && playerYLevelText != null)
        {
            // Get the Y position of the PlayerCube
            float playerYLevel = playerCube.transform.position.y;

            // Update the Text UI element
            playerYLevelText.text = playerYLevel.ToString("F2") + "m";
        }
    }
}
