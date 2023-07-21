using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField nameInputField;

    public void OnClickGameStartButton()
    {
        if(nameInputField.text != "")
        {
            PlayerSettings.userName = nameInputField.text;
            SceneManager.LoadScene("RoomListScene");
        } else
        {
            nameInputField.GetComponent<Animator>().SetTrigger("on");
        }
    }

    public void OnClickQuitButton()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
