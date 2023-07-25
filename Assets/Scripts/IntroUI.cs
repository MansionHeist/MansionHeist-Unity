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

    public PlayerController player;

    public void ShowPlayerType()
    {
        if (player != null)
        {
            if (player.playerType == EPlayerType.Guard){
                playerTypeText.text = "You Are Guard";
            } else{
                playerTypeText.text = "You Are Thief";
            }
        }
        else{
        Debug.LogError("Player reference is missing in IntroUI!");
        }
    }


    public IEnumerator ShowIntroSequence(){
        gameui.SetActive(false);
        intro.SetActive(true);
        ShowPlayerType();
        yield return new WaitForSeconds(3f);
        intro.SetActive(false);
        gameui.SetActive(true);
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
