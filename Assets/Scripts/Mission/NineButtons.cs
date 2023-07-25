using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NineButtons : MonoBehaviour
{
    public Button[] buttons;

    private int buttonsClicked = 0;

    private void Start()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            int buttonIndex = i;
            buttons[i].onClick.AddListener(() => OnButtonClicked(buttonIndex));
        }
    }

    private void OnButtonClicked(int buttonIndex)
    {
        buttonsClicked++;

        buttons[buttonIndex].gameObject.SetActive(false);

        if (buttonsClicked == buttons.Length)
        {
            AllButtonsAreClicked();
        }
    }

    private void AllButtonsAreClicked()
    {
        GameManager gameManager = GameManager.instance;
        gameManager.MissionDone(11);
        MissionUI missionUI = GetComponent<MissionUI>();
        missionUI.Close();
    }
}
