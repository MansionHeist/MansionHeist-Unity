using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RoomEnterButton : MonoBehaviour
{
    public void OnClickEnterRoom()
    {
        string roomNoString = gameObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text;
        ServerManager serverManager = GameObject.Find("ServerManager").GetComponent<ServerManager>();
        serverManager.emitMessage("room-list/enter-room", roomNoString);
        //SceneManager.LoadScene("RoomScene");
    }
}
