using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LockUI : MonoBehaviour
{
    public static LockUI Instance;

    public InputField passwordInput;
    public Button submitButton;
    public Text messageText;
    private GameManager gameManager; // Remove the public reference to GameManager

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

        gameObject.SetActive(false);
    }

    // Remove the 'locknum' parameter from this method
    public void DisplayMessage(string message, string password, int locknum)
    {
        messageText.text = message;
        passwordInput.text = "";
        // Set a callback for the submit button
        submitButton.onClick.RemoveAllListeners();
        submitButton.onClick.AddListener(() => CheckPassword(password, locknum));
        // Show the message UI with the text input field
        gameObject.SetActive(true);
    }

    // Add a new method to set the GameManager reference
    public void SetGameManager(GameManager manager)
    {
        gameManager = manager;
    }

    public void CloseMessage()
    {
        gameObject.SetActive(false);
    }

    private void CheckPassword(string correctPassword, int locknum)
    {
        string inputPassword = passwordInput.text;
        if (inputPassword == correctPassword)
        {
            // Password is correct, close the message UI and handle the item disappearance here
            CloseMessage();
            gameManager.HandleItemDisappear(2*locknum);
            gameManager.HandleItemDisappear(2*locknum+1);

        }
        else
        {
            messageText.text = "Incorrect password. Try again.";
        }
    }
}
