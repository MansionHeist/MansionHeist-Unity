using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ClosetLock : MonoBehaviour
{
    public string location;
    public Text messageText;
    public TMP_InputField passwordInput;
    public Button submitButton;
    public int locknum;

    public void Start()
    {
        passwordInput.text = "";
        messageText.color = Color.white;
        // Set a callback for the submit button
        submitButton.onClick.RemoveAllListeners();
        submitButton.onClick.AddListener(() => CheckPassword(GameManager.passwords[locknum]));
    }

    private void CheckPassword(string correctPassword)
    {
        string inputPassword = passwordInput.text;
        MissionUI missionUI = GetComponent<MissionUI>();
        if (inputPassword == correctPassword)
        {
            // Password is correct, close the message UI and handle the item disappearance here
            submitButton.gameObject.SetActive(false);
            messageText.text = "Correct!";

            missionUI.SuccessClose(locknum);
        }
        else
        {
            messageText.text = "Incorrect password.";
            messageText.color = Color.red;
            missionUI.FailClose(location);

        }
        
    }
}
