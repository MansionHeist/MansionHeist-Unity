using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField nameInputField;
    ServerManager serverManager;

    public void setNickname(string value)
    {
        PlayerSettings.userName = value;
        serverManager = GameObject.Find("ServerManager").GetComponent<ServerManager>();
        serverManager.emitMessage("setUserNickname", value);
        //SceneManager.LoadScene("RoomListScene");
    }

    public void OnClickGameStartButton()
    {
        if(nameInputField.text.Trim()!= "")
        {
            setNickname(nameInputField.text.Trim());
        } else
        {
            nameInputField.GetComponent<Animator>().SetTrigger("on");
        }
    }

    public void nextScene(){
        Debug.Log("nextScene");
        SceneManager.LoadScene("RoomListScene");
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
