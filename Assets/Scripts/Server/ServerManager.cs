using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using Firesplash.GameDevAssets.SocketIO;

public class ServerManager : MonoBehaviour{
    private string userSocketId = "";
    private string userName = "";
    //public SocketIOUnity socket = null;
    public SocketIOCommunicator sioCom;
    public bool isSocketConnected = false;
    //PlayerManager playerManager = null;
    
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
            //Debug.Log("initSocketId: " + data);
            initSocketId(data);
        });

        sioCom.Instance.On("playerMove", (string data) => {
            Debug.Log("playerMove: " + data);
            //playerManager.updatePlayerMovement(data);
        });

        sioCom.Instance.On("main-menu/finish-set-user-nickname", (string data) => {
            MainMenuUI mainMenuUI = GameObject.Find("MainMenuUI").GetComponent<MainMenuUI>();
            mainMenuUI.nextScene();
        });

        sioCom.Instance.On("room-list/set-room-info-list", (string data) => {
            RoomListViewManager roomListViewManager = GameObject.Find("Content").GetComponent<RoomListViewManager>();
            roomListViewManager.setRoomInfo(data);
        });
    }

    void initSocketId(String socketId){
        userSocketId = socketId.Trim();
        Debug.Log("My User Socket Id : " + socketId);
    }

    void Start(){
        InitSocket();
        DontDestroyOnLoad(this.gameObject);
        //playerManager = GameObject.Find("PlayerManager").GetComponent<PlayerManager>();
    }

    // Update is called once per frame
    private Queue<string> nameQueue = new Queue<string>();
    private Queue<string> messageQueue = new Queue<string>();
    void Update()
    {
        if(nameQueue.Count > 0){
            sioCom.Instance.Emit(nameQueue.Dequeue(), messageQueue.Dequeue(), true);
        }
    }

    public void emitMessage(string name, string message){
        if(isSocketConnected){
            nameQueue.Enqueue(name);
            messageQueue.Enqueue(message);
        }
    }
}
