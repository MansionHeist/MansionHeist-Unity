using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookShelf : MissionUI
{
    public List<int> correctSequence = new List<int> { 4, 1, 2, 3, 5, 6 };
    private List<int> buttonPresses = new List<int>();
    private int currentIndex = 0;

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
                SuccessClose(9);
            }
        }
        else
        {
            ResetSequence();
            FailClose("Library");
        }
        
        
    }

    private void ResetSequence()
    {
        buttonPresses.Clear();
        currentIndex = 0;
    }
}
