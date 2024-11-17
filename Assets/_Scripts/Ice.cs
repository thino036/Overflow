using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ice : MonoBehaviour
{
    public float lifeTime = 5f;
    private float fadeTime;
    private Material iceMat;
    private Color originalColor;

    // Start is called before the first frame update
    void Start()
    {
        iceMat = GetComponent<Renderer>().material;

        originalColor = iceMat.color;

        fadeTime = lifeTime - 1f;

        StartCoroutine(DestroyAfterTime());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator DestroyAfterTime()
    {
        float elapsedTime = 0f;

        while (elapsedTime < lifeTime)
        {
            elapsedTime += Time.deltaTime; 

            // Start fading when the timer exceeds fadeTime
            if (elapsedTime >= fadeTime)
            {
                float fadeProgress = (elapsedTime - fadeTime) / (lifeTime - fadeTime);
                SetPlatformTransparency(1f - fadeProgress);
            }

            yield return null;
        }

        Destroy(gameObject);
    }

    private void SetPlatformTransparency(float alpha)
    {
        Color color = originalColor;
        color.a = alpha;
        iceMat.color = color;
    }
}
