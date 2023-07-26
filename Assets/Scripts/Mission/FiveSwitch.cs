using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FiveSwitch : MonoBehaviour
{
    private int buttonsPressed = 0;
    private int totalButtons = 5;
    [SerializeField] public List<Button> buttons; // List of buttons to track

    private void Start()
    {
        // Add onClick listeners to all buttons in the list
        foreach (Button button in buttons)
        {
            button.onClick.AddListener(() => OnButtonClicked(button));
        }
    }

    private void OnButtonClicked(Button button)
    {
        // Increase the count of pressed buttons
        buttonsPressed++;

        // Disable the button to make it unclickable
        button.interactable = false;

        // Check if all buttons are pressed
        if (buttonsPressed == totalButtons)
        {
            AllButtonsArePressed();
        }
    }

    private void AllButtonsArePressed()
    {
        MissionUI missionUI = GetComponent<MissionUI>();
        missionUI.SuccessClose(8);
    }
}
