using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class MessageUIController : MonoBehaviour
{
    public static MessageUIController Instance;

    public Text messageText;

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

    public void DisplayMessage(string message)
    {
        messageText.text = message;
        gameObject.SetActive(true);
    }

    public void CloseMessage()
    {
        gameObject.SetActive(false);
    }
}
