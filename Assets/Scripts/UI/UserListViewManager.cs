using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UserListViewManager : MonoBehaviour
{
    public GameObject listItem; // ??????? ??????
    public RectTransform contentTransform; // ??????? ????????? ??????? ??? Content Transform

    private int rows = 5;
    private int columns = 2;

    // ?????????? ???? ????????? ?????? ?????

    // Start is called before the first frame update
    void Start()
    {        
        // ??��?? ???????? ????????? ??????? ????
        // ?????????? ????????? ????? nicknames ??????? ???????? ????
        getUserList();
        /*nicknames = new List<string>
        {
            "Player1", "Player2", "Player3", "Player4", "Player5",
            "Player6", "Player7", "Player8", "Player9", "Player10"
        };
        CreateListView();*/
    }

    private class UserInfo{
        public List<string> userNameList;
        public List<bool> userReadyList;
        public string masterUserName;
        public string roomName;
    }

    public void setUserList(string json){
        UserInfo userInfo = JsonUtility.FromJson<UserInfo>(json);
        TextMeshProUGUI roomNameText = GameObject.Find("RoomUI").transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        roomNameText.text = userInfo.roomName;
        CreateListView(userInfo);
    }

    public void getUserList(){
        ServerManager serverManager = GameObject.Find("ServerManager").GetComponent<ServerManager>();
        serverManager.emitMessage("user-list/get-user-list", "");
    }

    private void CreateListView(UserInfo userInfo)
    {
        Debug.Log("CreateListView");
        foreach(Transform child in transform){
            Destroy(child.gameObject);
        }
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                int index = row * columns + col;
                if (index >= userInfo.userNameList.Count){
                    row = rows+1;
                    break; // ??????? ?????? ?????????? ??
                }

                // ??????? ??? ?????? ????
                GameObject item = Instantiate(listItem, contentTransform);

                TextMeshProUGUI userNameText = item.GetComponent<TextMeshProUGUI>();
                if(userNameText!=null){
                    if(userInfo.masterUserName == userInfo.userNameList[index]){
                        userNameText.text = userInfo.userNameList[index];
                        userNameText.color = Color.red;
                    }
                    else if(userInfo.userReadyList[index]){
                        userNameText.text = userInfo.userNameList[index] + " (Ready)";
                        userNameText.color = Color.green;
                    }else{
                        userNameText.text = userInfo.userNameList[index];
                    }
                }
            }
        }
        Debug.Log(userInfo.masterUserName);
        GameObject readyButton = GameObject.Find("PopupBackground").transform.GetChild(1).gameObject;
        GameObject startButton = GameObject.Find("PopupBackground").transform.GetChild(2).gameObject;
        if(userInfo.masterUserName == PlayerSettings.userName){
            startButton.SetActive(true);
            readyButton.SetActive(false);
        }else{
            startButton.SetActive(false);
            readyButton.SetActive(true);
        }
    }
}
