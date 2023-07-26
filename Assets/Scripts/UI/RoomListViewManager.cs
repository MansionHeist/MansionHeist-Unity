using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RoomListViewManager : MonoBehaviour
{
    public GameObject listItem; // ?��?��?��?��?��?��?��?��?��?�� ?��?��?��?��?��?��?��?��?��
    public RectTransform contentTransform; // ?��?��?��?��?��?��?��?��?��?�� ?��?��?��?��?��?��?��?��?��?��?��?��?�� ?��?��?��?��?��?��?��?��?��?�� ?��?��?��載� Content Transform

    private int rows = 100;
    private int columns = 2;

    private List<string> serNameList;
    private List<int> userNoList; // ?��?��?��?��?��?��?��?��븝옙?��?��?�� ?��?��?��?��?�� ?��?��?��?��?��?��?��?��?��?��?�� ?��?��?��?��?��?��?��?��?�� ?��?��?��?��?��?��?��

    // Start is called before the first frame update
    void Start()
    {        
        // ?��?��?��뤄옙 ?��?��?��?��?��?��?��?��?��?��?��?�� ?��?��?��?��?��?��?��?��?��?��?�� ?��?��?��?��?��곤옙 ?��?��?��?��?��?��
        // ?��?��?��?��?��?��?��?��븝옙?��?��?�� ?��?��?��?��?��?��?��?��?��?��?�� ?��?��?��????��?�� nicknames ?��?��?��?��?��?��?��?��?��?�� ?��?��?��?��?��?��?��?��?��곤옙 ?��?��?��?��?��?��
        getRoomInfo();
        /*nicknames = new List<string>
        {
            "Player1", "Player2", "Player3", "Player4", "Player5",
            "Player6", "Player7", "Player8", "Player9", "Player10"
        };
        CreateListView();*/
    }

    public void getRoomInfo(){
        ServerManager serverManager = GameObject.Find("ServerManager").GetComponent<ServerManager>();
        serverManager.emitMessage("room-list/get-room-info-list", "");
    }

    private class RoomInfo{
        public List<string> roomNameList;
        public List<string> roomUserNameList;
        public List<int> roomUserNoList;
        public List<int> roomIDList;
    }

    public void setRoomInfo(string data){
        //Debug.Log("setUserName");
        Debug.Log(data);
        RoomInfo roomInfo = JsonUtility.FromJson<RoomInfo>(data);
        CreateListView(roomInfo);
    }

    private void CreateListView(RoomInfo roomInfo)
    {
        foreach(Transform child in transform){
            Destroy(child.gameObject);
        }
        Debug.Log(contentTransform.rect.width.ToString());
        Debug.Log(contentTransform.rect.height.ToString());
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                int index = row * columns + col;
                if (index >= roomInfo.roomNameList.Count)
                    return; // ?��?��?��?��?��?��?��?��?��?�� ?��?��?��?��?��?��?��?��?�� ?��?��?��?��?��?��?��?��?��?��?��?��몌옙 ?��?��?��?��

                // ?��?��?��?��?��?��?��?��?��?�� ?��?��몌옙 ?��?��?��?��?��?��?��?��?�� ?��?��?��?��?��?��
                GameObject item = Instantiate(listItem, contentTransform);

                // ?��?��몌옙?��?��?�� ?��?��?��치占?��?�� ?��?��?��?�� ?��?��?��?��?��?��
                RectTransform itemRect = item.GetComponent<RectTransform>();
                //float itemWidth = contentTransform.rect.width / columns;
                //float itemHeight = contentTransform.rect.height / rows;
                //itemRect.sizeDelta = new Vector2(itemWidth, itemHeight);
                //itemRect.anchoredPosition = new Vector2(col * itemWidth, -row * itemHeight);

                // ?��?��?��?��?��?�� ?��?��?��?��?��?��?�� ?��?��?��?��?��?��?��?��?��?�� UI ?��?��?��?��?��?��?��?���? ?��?��몌옙 채占?��?��求占�?.
                // ?��?��?��?��?��?�� ?��?��?��?���?, Text ?��?��?��?��?��?��?��?��?��?��?��?��?�� ?��?��?��?��?��?��?��?��?��?�� ?��?��?��?��?��?��?��?�� ?��?��?��?��?��?��?��?��거놂?��, ?��?��뱄옙?��?��?��?��?��?�� ?��?��?��?��?��?��?��?��?��?�� ?��?��?��?��?��?�� ?��?��?��?��?��?��?�� ?��?��?��?��?��?��?��?��?�� ?��?��?�� ?��?��?��?��?��?��?��?��.
                TextMeshProUGUI roomNameText = item.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
                TextMeshProUGUI nicknameText = item.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
                TextMeshProUGUI userNoText = item.transform.GetChild(3).GetComponent<TextMeshProUGUI>();
                TextMeshProUGUI roomIDText = item.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
                //TextMeshPro textComponent = item.GetComponentInChildren<TextMeshPro>();
                if (roomNameText != null){
                    //Debug.Log("roomNameText");
                    roomNameText.text = roomInfo.roomNameList[index];//"Room " + index.ToString();
                }
                if(nicknameText!=null){
                    nicknameText.text = roomInfo.roomUserNameList[index];//"User " + index.ToString();
                }
                if(userNoText != null){
                    userNoText.text = roomInfo.roomUserNoList[index].ToString() + "/10";//"User No " + index.ToString();
                }
                if(roomIDText != null){
                    roomIDText.text = roomInfo.roomIDList[index].ToString();
                }
            }
        }
    }
}
