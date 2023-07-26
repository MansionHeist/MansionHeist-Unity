using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using Firesplash.GameDevAssets.SocketIO;
using UnityEngine.SceneManagement;

public class ServerManager : MonoBehaviour{
    private string userSocketId = "";
    //public SocketIOUnity socket = null;
    public SocketIOCommunicator sioCom;
    public bool isSocketConnected = false;
    PlayerManager playerManager = null;
    private int userRoomIdx = -1;

    public void setUserRoomIdx(int _userRoomIdx){
        userRoomIdx = _userRoomIdx;
    }
    
    public int getUserRoomIdx(){
        return userRoomIdx;
    }
    
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
            if(roomListViewManager!=null){
                roomListViewManager.setRoomInfo(data);
            }
        });

        sioCom.Instance.On("room-list/enter-room", (string data) => {
            if(data=="success"){
                SceneManager.LoadScene("RoomScene");
            }else{
                RoomListViewManager roomListViewManager = GameObject.Find("Content").GetComponent<RoomListViewManager>();
                if(roomListViewManager!=null){
                    roomListViewManager.getRoomInfo();
                }
            }
        });
        sioCom.Instance.On("user-list/set-user-list", (string data) => {
            Debug.Log("set-user-list: " + data);
            UserListViewManager userListViewManager = GameObject.Find("JoiningPeopleList").GetComponent<UserListViewManager>();
            if(userListViewManager!=null){
                userListViewManager.setUserList(data);
            }
        });
        sioCom.Instance.On("user-list/start-game", (string data) => {
            SceneManager.LoadScene("LoadingScene");
        });
        sioCom.Instance.On("loading/set-user-info", (string data) => {
            LoadingSceneController loadingSceneController = GameObject.Find("Canvas").GetComponent<LoadingSceneController>();
            if(loadingSceneController!=null){
                loadingSceneController.setUserInfo(data);
            }
        });
        sioCom.Instance.On("loading/start-game", (string data) => {
            LoadingSceneController loadingSceneController = GameObject.Find("Canvas").GetComponent<LoadingSceneController>();
            if(loadingSceneController!=null){
                loadingSceneController.loadScene("MansionMap");
            }
        });
        sioCom.Instance.On("game/user-movement", (string data) => {
            Debug.Log("! game/user-movement: " + data);
            if(playerManager == null){
                playerManager = GameObject.Find("PlayerManager").GetComponent<PlayerManager>();
            }
            playerManager.updatePlayerMovement(data);
        });
        sioCom.Instance.On("game/mission-done", (string data) => {
            Debug.Log("! game/mission-done: " + data);
            GameManager gameManager = GameManager.instance;
            gameManager.MissionDone(int.Parse(data));
        });
        sioCom.Instance.On("game/arrest-theif", (string data) => {
            /*if(data==PlayerSettings.userName){
                PlayerController playerController = GameObject.Find("Player").GetComponent<PlayerController>();
                playerController.Caught();
            }else{*/
                PlayerManager playerManager = GameObject.Find("PlayerManager").GetComponent<PlayerManager>();
                playerManager.arrestTheif(data);
            //}
        });
        sioCom.Instance.On("game/unlock-theif", (string data) => {
            /*if(data==PlayerSettings.userName){
                PlayerController playerController = GameObject.Find("Player").GetComponent<PlayerController>();
                playerController.GetOutOfJail();
            }else{*/
                PlayerManager playerManager = GameObject.Find("PlayerManager").GetComponent<PlayerManager>();
                playerManager.unlock(data);
            //}
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
            sioCom.Instance.Emit(name, message, true);
            //Debug.Log("emitMessage: " + name + ", " + message);
            //nameQueue.Enqueue(name);
            //messageQueue.Enqueue(message);
        }
    }

    public void emitMessage2(string name, string message){
        if(isSocketConnected){
            sioCom.Instance.Emit(name, message, false);
            //Debug.Log("emitMessage: " + name + ", " + message);
            //nameQueue.Enqueue(name);
            //messageQueue.Enqueue(message);
        }
    }
}
