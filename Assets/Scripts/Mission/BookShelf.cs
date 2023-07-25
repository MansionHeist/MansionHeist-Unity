using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookShelf : MonoBehaviour
{
    public List<int> correctSequence = new List<int> { 4, 1, 2, 3, 5, 6 };
    private List<int> buttonPresses = new List<int>();
    private int currentIndex = 0;

    public AlarmUI alarmUI;

    public void OnButtonClick(int buttonNumber)
    {
        buttonPresses.Add(buttonNumber);

        if (buttonPresses.Count > correctSequence.Count)
        {
            // Remove oldest button press if more than the correct sequence length
            buttonPresses.RemoveAt(0);
        }

        // Check if the current button press matches the correct sequence
        if (buttonPresses[buttonPresses.Count - 1] == correctSequence[currentIndex])
        {
            currentIndex++;

            if (currentIndex == correctSequence.Count)
            {
                // Success! The correct sequence was pressed.
                Debug.Log("Success");
                GameManager gameManager = GameManager.instance;
                gameManager.MissionDone(9);
                MissionUI missionUI = GetComponent<MissionUI>();
                missionUI.Close();

            }
        }
        else
        {
            // Incorrect button press, reset the sequence and set the alarm
            alarmUI.StartFlickering("Library");
            ResetSequence();
            MissionUI missionUI = GetComponent<MissionUI>();
            missionUI.Close();
        }
        
        
    }

    private void ResetSequence()
    {
        buttonPresses.Clear();
        currentIndex = 0;
    }
}
