using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionCompleteUI : MonoBehaviour
{
    public Animator imageAnimator;

    public void Show()
    {
        StartCoroutine(ActivateAndDeactivateUI());
    }

    private IEnumerator ActivateAndDeactivateUI()
    {
        // Activate the UI
        gameObject.SetActive(true);
        imageAnimator.Play("SlideInAnimation"); // Play the SlideInAnimation

        // Wait for 0.5 seconds
        yield return new WaitForSeconds(0.5f);

        // Deactivate the UI
        imageAnimator.Play("SlideOutAnimation"); // Play the SlideOutAnimation

        // Wait for the SlideOutAnimation to finish before deactivating the GameObject
        yield return new WaitForSeconds(imageAnimator.GetCurrentAnimatorStateInfo(0).length);

        gameObject.SetActive(false);
    }

}
