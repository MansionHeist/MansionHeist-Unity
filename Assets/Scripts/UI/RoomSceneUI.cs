using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSceneUI : MonoBehaviour
{
    bool isReady = false;
    public void OnClickReadyButton()
    {
        Debug.Log("OnClickReadyButton");
        //LoadingSceneController.LoadScene("MansionMap");
        if(isReady){
            isReady = false;
            ServerManager serverManager = GameObject.Find("ServerManager").GetComponent<ServerManager>();
            serverManager.emitMessage("user-list/user-not-ready", "");
        }else{
            isReady = true;
            ServerManager serverManager = GameObject.Find("ServerManager").GetComponent<ServerManager>();
            serverManager.emitMessage("user-list/user-ready", "");
        }
    }

    public void OnClickStartButton()
    {
        ServerManager serverManager = GameObject.Find("ServerManager").GetComponent<ServerManager>();
        serverManager.emitMessage("user-list/start-game", "");
        //LoadingSceneController.LoadScene("MansionMap");
    }
}
