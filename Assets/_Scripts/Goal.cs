using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public GameObject EndGameCanvas;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            EndGameCanvas.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}
