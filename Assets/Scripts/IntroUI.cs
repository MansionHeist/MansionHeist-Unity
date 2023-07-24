using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroUI : MonoBehaviour
{
    [SerializeField] private Text playerTypeText;
    [SerializeField] private GameObject intro;
    [SerializeField] private GameObject gameui;

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
        intro.SetActive(true);
        gameui.SetActive(false);
        ShowPlayerType();
        yield return new WaitForSeconds(3f);
        intro.SetActive(false);
        gameui.SetActive(true);
    }


}
