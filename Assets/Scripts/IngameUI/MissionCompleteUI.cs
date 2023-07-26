using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionCompleteUI : MonoBehaviour
{
    public Animator imageAnimator;

    public void Show()
    {
        gameObject.SetActive(true);
        StartCoroutine(ActivateAndDeactivateUI());
    }

    private IEnumerator ActivateAndDeactivateUI()
    {
        yield return new WaitForSeconds(1.3f);
      // Deactivate the GameObject
        gameObject.SetActive(false);
    }
}
