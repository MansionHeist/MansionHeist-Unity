using System.Collections;
using System.Collections.Generic;
using SocketIOClient;
using SocketIOClient.Newtonsoft.Json;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;


public class PlayerManager : MonoBehaviour
{
    [SerializeField] public GameObject playerPrefabs = null;
    private GameObject currentPlayer = null;
    private SocketSystem socketSystem = null;
    private List<GameObject> playerList = new List<GameObject>();
    private List<String> userIdList = new List<String>();
    // Start is called before the first frame update
    void Start()
    {
        socketSystem = GameObject.Find("SocketSystem").GetComponent<SocketSystem>();
        currentPlayer = GameObject.Find("guard1_walk_0");
    }

    // Update is called once per frame
    void Update()
    {
        sendUserMovement();
    }

    public class PlayerData{
        public float[] position;
        public float[] scale;
        public float[] rotation;
        public string userId;
    }

    public void sendUserMovement(){
        PlayerData playerData = new PlayerData();
        Transform transform = currentPlayer.transform;
        Vector3 position = transform.position;
        playerData.position = new float[3]{
            position.x,
            position.y,
            position.z
        };
        Vector3 scale = transform.localScale;
        playerData.scale = new float[3]{
            scale.x,
            scale.y,
            scale.z
        };
        Vector3 rotation = transform.rotation.eulerAngles;
        playerData.rotation = new float[3]{
            rotation.x,
            rotation.y,
            rotation.z
        };
        playerData.userId = socketSystem.getCurrentSocketId();
        string json = JsonConvert.SerializeObject(playerData);
        socketSystem.emitMessage("playerMove", json);
    }

    void addUser(String userId){
        var player = UnityEngine.Object.Instantiate(playerPrefabs, Vector3.zero, Quaternion.identity, null) as GameObject;
        player.gameObject.name = userId;
        player.GetComponent<CharacterMover>().isCurrentPlayer = false;
        playerList.Add(player);
        userIdList.Add(userId);
    }

    public void updatePlayerMovement(string json){
        PlayerData playerData = JsonConvert.DeserializeObject<PlayerData>(json);
        Vector3 position = new Vector3(playerData.position[0],playerData.position[1],playerData.position[2]);
        Vector3 scale = new Vector3(playerData.scale[0],playerData.scale[1],playerData.scale[2]);
        Vector3 rotation = new Vector3(playerData.rotation[0],playerData.rotation[1],playerData.rotation[2]);
        bool foundUser = false;
        for(int i=0; i<userIdList.Count; i++){
            if(userIdList[i] == playerData.userId){
                foundUser = true;
                playerList[i].transform.position = position;
                playerList[i].transform.localScale = scale;
                playerList[i].transform.rotation = Quaternion.Euler(rotation);
                break;
            }
        }
        if(!foundUser){
            addUser(playerData.userId);
            playerList[playerList.Count-1].transform.position = position;
            playerList[playerList.Count-1].transform.localScale = scale;
            playerList[playerList.Count-1].transform.rotation = Quaternion.Euler(rotation);
        }
    }
}
