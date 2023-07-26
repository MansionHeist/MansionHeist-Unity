using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AlarmUI : MonoBehaviour
{
    public Image flickerImage;
    public TMP_Text location;
    public float flickerSpeed = 0.2f;
    public float flickerDuration = 3f;
    private bool isFlickering = false;


    public void StartFlickering(string location)
    {
        gameObject.SetActive(true);
        StartCoroutine(FlickerCoroutine(location));
        
    }

    private void StopFlickering(string location)
    {
        if (isFlickering)
        {
            StopCoroutine(FlickerCoroutine(location));
            isFlickering = false;
        }
    }

    private IEnumerator FlickerCoroutine(string location)
    {
        this.location.text = location;
        isFlickering = true;
        float elapsedTime = 0f;

        while (elapsedTime < flickerDuration)
        {
            Color newColor = flickerImage.color;
            newColor.a = 0;
            flickerImage.color = newColor;
            yield return new WaitForSeconds(flickerSpeed);
            newColor.a = 0.5f;
            flickerImage.color = newColor;
            yield return new WaitForSeconds(flickerSpeed);

            elapsedTime += 2*flickerSpeed;
        }

        // Ensure the final color is set to the desired alpha value
        Color finalColor = flickerImage.color;
        finalColor.a = 0;
        flickerImage.color = finalColor;

        isFlickering = false;
        gameObject.SetActive(false);
    }
}
