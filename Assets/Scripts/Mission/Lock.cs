using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClosetLock : MonoBehaviour
{
    public static ClosetLock Instance;
    public string location;
    public Text messageText;
    public InputField passwordInput;
    public Button submitButton;
    public AlarmUI alarm;
    public int locknum;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

        }
        else
        {
            Destroy(gameObject);
        }
    }

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
        if (inputPassword == correctPassword)
        {
            // Password is correct, close the message UI and handle the item disappearance here
            submitButton.gameObject.SetActive(false);
            messageText.text = "Correct!";
            GameManager gameManager = GameManager.instance;
            gameManager.MissionDone(2 * locknum);
            gameManager.MissionDone(2 * locknum+1);
        }
        else
        {
            messageText.text = "Incorrect password.";

            messageText.color = Color.red;
            alarm.StartFlickering(location);

        }
        MissionUI missionUI = GetComponent<MissionUI>();
        missionUI.Close();
    }
}
