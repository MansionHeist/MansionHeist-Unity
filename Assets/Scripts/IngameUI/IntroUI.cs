using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroUI : MonoBehaviour
{
    public float timerDuration = 3f;
    [SerializeField] private Text playerTypeText;
    [SerializeField] private GameObject intro;
    [SerializeField] private GameObject gameui;
    [SerializeField] private Text timerText;

    private void Awake()
    {
        timerDuration = PlayerSettings.userType == EPlayerType.Thief ? timerDuration : 8f;
    }

    public void ShowPlayerType()
    {
        if (PlayerSettings.userType == EPlayerType.Guard){
            playerTypeText.text = "You Are Guard";
        } else{
            playerTypeText.text = "You Are Thief";
        }
    }

    public IEnumerator ShowIntroSequence(){
        PlayerController.isMoveable = false;
        gameui.SetActive(false);
        intro.SetActive(true);
        ShowPlayerType();
        yield return new WaitForSeconds(timerDuration);
        intro.SetActive(false);
        gameui.SetActive(true);
        PlayerController.isMoveable = true;
    }

    public IEnumerator TimerCoroutine()
    {
        float remainingTime = timerDuration;

        while (remainingTime > 0f)
        {
            // Update the timer text with the remaining time
            timerText.text = Mathf.CeilToInt(remainingTime).ToString();
            yield return new WaitForSeconds(1f);
            remainingTime -= 1f;
        }
    }
}
