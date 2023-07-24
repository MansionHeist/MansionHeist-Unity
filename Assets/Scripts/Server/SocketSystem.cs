using System.Collections;
using System.Collections.Generic;
using SocketIOClient;
using SocketIOClient.Newtonsoft.Json;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using Firesplash.GameDevAssets.SocketIO;

public class SocketSystem : MonoBehaviour
{
    
    private string userSocketId = "";
    //public SocketIOUnity socket = null;
    public SocketIOCommunicator sioCom;
    public bool isSocketConnected = false;
    PlayerManager playerManager = null;
    
    public String getCurrentSocketId(){
        return userSocketId;
    }

    void InitSocket(){
        sioCom = GetComponent<SocketIOCommunicator>();
        Debug.Log("Socket Init");
         sioCom.Instance.On("connect", (string data) => {
            Debug.Log("LOCAL: Hey, we are connected!");
            isSocketConnected = true;
        });
        sioCom.Instance.Connect();
        setSocketMessageHandler();
    }

    void setSocketMessageHandler(){
        sioCom.Instance.On("initSocketId", (string data) => {
            Debug.Log("initSocketId: " + data);
            initSocketId(data);
        });

        sioCom.Instance.On("playerMove", (string data) => {
            Debug.Log("playerMove: " + data);
            playerManager.updatePlayerMovement(data);
        });
    }

    void initSocketId(String socketId){
        userSocketId = socketId.Trim();
        Debug.Log("My User Socket Id : " + socketId);
    }

    void Start()
    {
        InitSocket();
        playerManager = GameObject.Find("PlayerManager").GetComponent<PlayerManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void emitMessage(string name, string message){
        if(isSocketConnected){
            sioCom.Instance.Emit(name, message, false);
        }
    }
}
