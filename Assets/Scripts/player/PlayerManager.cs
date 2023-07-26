using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] public GameObject theifPrefab = null, guardPrefab = null;
    //private ServerManager serverManager = null;
    private List<string> userNameList = new List<string>();
    private List<GameObject> playerList = new List<GameObject>();
    private int theifNo = 0, caughtNo = 0;


    void Start(){
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update(){
        
    }

    private class PlayerData{
        public float[] position;
        public float[] rotation;
        public string userName;
        public bool isMove;
        public int userRoomIdx;
    }

    public void addUser(string userName, string userType){
        GameObject player = null;
        if(userType=="theif"){
            player = Instantiate(theifPrefab, new Vector3(0f, 0f, 0f), Quaternion.identity);
        }else{
            player = Instantiate(guardPrefab, new Vector3(0f, 0f, 0f), Quaternion.identity);
        }
        player.SetActive(false);
        DontDestroyOnLoad(player);
        OtherPlayerController otherPlayerController = player.GetComponent<OtherPlayerController>();
        otherPlayerController.setUserName(userName);
        otherPlayerController.setUserType(userType);
        playerList.Add(player);
    }

    public void initPlayerList(List<string> userNameList, List<string> userTypeList){
        Debug.Log("initPlayerList : " + userNameList.Count);
        theifNo = 0;
        caughtNo = 0;
        for(int i=0; i<userNameList.Count; i++){
            Debug.Log(userNameList[i] + " : " + userTypeList[i]);
            if(userTypeList[i]=="theif"){
                theifNo++;
            }
            if(userNameList[i]!=PlayerSettings.userName){
                addUser(userNameList[i], userTypeList[i]);
            }
            else{
                playerList.Add(null);
            }
        }
    }

    public void startGame(){
        for(int i=0; i<playerList.Count; i++){
            if(playerList[i]!=null)
                playerList[i].SetActive(true);
        }
    }

    public void updatePlayerMovement(string json){
        PlayerData playerData = JsonUtility.FromJson<PlayerData>(json);
        if(playerData.userName == PlayerSettings.userName){
            return;
        }
        Debug.Log("updatePlayerMovement: " + json);
        Vector3 position = new Vector3(playerData.position[0],playerData.position[1],playerData.position[2]);
        Vector3 rotation = new Vector3(playerData.rotation[0],playerData.rotation[1],playerData.rotation[2]);
        int userIdx = playerData.userRoomIdx;
        playerList[userIdx].transform.position = position;
        playerList[userIdx].transform.rotation = Quaternion.Euler(rotation);
        playerList[userIdx].GetComponent<Animator>().SetBool("isMove", playerData.isMove);
    }

    public void arrestTheif(string userName){
        if(userName==PlayerSettings.userName){
            PlayerController playerController = GameObject.Find("Player").GetComponent<PlayerController>();
            playerController.Caught();
        }else{
            for(int i=0; i<playerList.Count; i++){
                if(playerList[i]!=null && playerList[i].GetComponent<OtherPlayerController>().getUserName()==userName){
                    playerList[i].GetComponent<OtherPlayerController>().Caught();
                }
            }
        }
        caughtNo = GameObject.FindGameObjectsWithTag("CaughtPlayer").Length;
        if(theifNo==caughtNo){
            GameManager.instance.SetGameOver((PlayerSettings.userType == EPlayerType.Guard));
        }
    }
    public void unlock(string userName){
        if(userName==PlayerSettings.userName){
            PlayerController playerController = GameObject.Find("Player").GetComponent<PlayerController>();
            playerController.GetOutOfJail();
        }else{
            for(int i=0; i<playerList.Count; i++){
                if(playerList[i]!=null && playerList[i].GetComponent<OtherPlayerController>().getUserName()==userName){
                    playerList[i].GetComponent<OtherPlayerController>().GetOutOfJail();
                }
            }
        }
    }
}
