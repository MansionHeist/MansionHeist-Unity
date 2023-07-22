using System.Collections;
using System.Collections.Generic;
using SocketIOClient;
using SocketIOClient.Newtonsoft.Json;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;

public class SocketSystem : MonoBehaviour
{
    private string userSocketId = "";
    public SocketIOUnity socket = null;
    public bool isSocketConnected = false;
    PlayerManager playerManager = null;
    
    public String getCurrentSocketId(){
        return userSocketId;
    }

    void InitSocket(){
        // Debug.Log("Socket Init");
        var uri = new Uri("http://158.247.249.12:80");
        socket = new SocketIOUnity(uri, new SocketIOOptions{
            Query = new Dictionary<string, string>
                {
                    {"token", "UNITY" }
                }
            ,
            EIO = 4
            ,
            Transport = SocketIOClient.Transport.TransportProtocol.WebSocket
        });
        socket.JsonSerializer = new NewtonsoftJsonSerializer();

        socket.OnConnected += (sender, e) =>
        {
            Debug.Log("socket.OnConnected");
            isSocketConnected = true;
        };
        socket.OnDisconnected += (sender, e) =>
        {
            Debug.Log("disconnect: " + e);
        };
        
        socket.Connect();
        setSocketMessageHandler();
    }

    void setSocketMessageHandler(){
        socket.OnAnyInUnityThread((name, response) => {
            Debug.Log("OnAnyInUnityThread: " + name + " " + response.GetValue<String>());
            string json = response.GetValue<String>();
            if(name == "initSocketId"){
                initSocketId(json);
            }
            else if(name=="playerMovement"){
                playerManager.updatePlayerMovement(json);
            }
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
            socket.Emit(name, message);
        }
    }
}
