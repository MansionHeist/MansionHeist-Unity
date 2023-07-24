using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LockUI : MonoBehaviour
{
    public static LockUI Instance;

    public InputField passwordInput;
    public Button submitButton;
    public Text messageText;
    public LockController lockController;

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

    public void DisplayMessage(string message, string password)
    {
        messageText.text = message;
        passwordInput.text = "";
        // Set a callback for the submit button
        submitButton.onClick.RemoveAllListeners();
        submitButton.onClick.AddListener(() => CheckPassword(password));
        // Show the message UI with the text input field
        gameObject.SetActive(true);
    }

    public void CloseMessage()
    {
        gameObject.SetActive(false);
    }

    private void CheckPassword(string correctPassword)
    {
        string inputPassword = passwordInput.text;
        if (inputPassword == correctPassword)
        {
            // Password is correct, close the message UI and handle the item disappearance here
            CloseMessage();
            lockController.HandleItemDisappear();
        }
        else
        {
            messageText.text = "Incorrect password. Try again.";
        }
    }
}
