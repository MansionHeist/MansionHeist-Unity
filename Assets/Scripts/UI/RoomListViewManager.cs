using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RoomListViewManager : MonoBehaviour
{
    public GameObject listItem; // ? ?™?˜™? ?™?˜™?Š¸? ?™?˜™ ? ?™?˜™? ?™?˜™? ?™?˜™
    public RectTransform contentTransform; // ? ?™?˜™? ?™?˜™?Š¸? ?™?˜™ ? ?™?˜™? ?™?˜™? ?Œœ?“¸?˜™? ?™?˜™ ? ?Œ˜?™?˜™? ?™?˜™? ?™?˜™ ? ?™?˜™è¼‰ï¿½ Content Transform

    private int rows = 100;
    private int columns = 2;

    private List<string> nicknameList, roomNameList;
    private List<int> userNoList; // ? ?™?˜™? ?™?˜™? ?‹¸ë¸ì˜™? ?™?˜™ ? ?Œ¨?•„?š¸?˜™ ? ?‹»?†‚?˜™? ?Œˆ?“¸?˜™? ?™?˜™ ? ?™?˜™? ?™?˜™? ?™?˜™ ? ?™?˜™? ?™?˜™?Š¸

    // Start is called before the first frame update
    void Start()
    {        
        // ? ?Œˆ?‹œë¤„ì˜™ ? ?™?˜™? ?™?˜™? ?™?˜™? ?™?˜™ ? ?‹»?†‚?˜™? ?Œˆ?“¸?˜™? ?™?˜™ ? ?Œ¨?•„?˜¨?‹¤ê³¤ì˜™ ? ?™?˜™? ?™?˜™
        // ? ?™?˜™? ?™?˜™? ?‹¸ë¸ì˜™? ?™?˜™ ? ?‹»?†‚?˜™? ?Œˆ?“¸?˜™? ?™?˜™ ? ?Œ¨?•„????‡½?˜™ nicknames ? ?™?˜™? ?™?˜™?Š¸? ?™?˜™ ? ?™?˜™? ?™?˜™? ?‹¼?‹¤ê³¤ì˜™ ? ?™?˜™? ?™?˜™
        getRoomInfo();
        /*nicknames = new List<string>
        {
            "Player1", "Player2", "Player3", "Player4", "Player5",
            "Player6", "Player7", "Player8", "Player9", "Player10"
        };
        CreateListView();*/
    }

    private void getRoomInfo(){
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
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                int index = row * columns + col;
                if (index >= roomInfo.roomNameList.Count)
                    return; // ? ?™?˜™? ?™?˜™?Š¸? ?™?˜™ ? ?™?˜™? ?™?˜™? ?™?˜™ ? ?™?˜™? ?™?˜™? ?‹¤?–µ?˜™? ?Œ•ëªŒì˜™ ? ?Œ©?Œ?˜™

                // ? ?™?˜™? ?™?˜™?Š¸? ?™?˜™ ? ?Œ“ëªŒì˜™ ? ?™?˜™? ?™?˜™? ?™?˜™ ? ?™?˜™? ?™?˜™
                GameObject item = Instantiate(listItem, contentTransform);

                // ? ?Œ“ëªŒì˜™? ?™?˜™ ? ?™?˜™ì¹˜å ?™?˜™ ?¬? ?™?˜™ ? ?™?˜™? ?™?˜™
                RectTransform itemRect = item.GetComponent<RectTransform>();
                float itemWidth = contentTransform.rect.width / columns;
                float itemHeight = contentTransform.rect.height / rows;
                itemRect.sizeDelta = new Vector2(itemWidth, itemHeight);
                itemRect.anchoredPosition = new Vector2(col * itemWidth, -row * itemHeight);

                // ? ?™?˜™? ?©?„œ ? ?‹­?š¸?˜™? ?™?˜™ ? ?™?˜™? ?™?˜™? ?‹¶?†‚?˜™ UI ? ?™?˜™?’š? ?™?˜™? ï¿? ? ?Œ“ëªŒì˜™ ì±„å ?™?˜™æ±‚å ï¿?.
                // ? ?™?˜™? ?™?˜™ ? ?™?˜™? ï¿?, Text ? ?™?˜™? ?™?˜™? ?™?˜™?Š¸? ?™?˜™ ? ?™?˜™? ?™?˜™? ?‹¶?‡½?˜™ ? ?Œ”?™?˜™?Š¸? ?™?˜™ ? ?™?˜™? ?™?˜™? ?‹¹ê±°ë†‚?˜™, ? ?‹±ë±„ì˜™? ?™?˜™? ?™?˜™ ? ?™?˜™? ?™?˜™? ?‹¹?Œ?˜™ ? ?™?˜™? ?™?˜™ ? ?Œœ?–µ?˜™? ?™?˜™ ? ?™?˜™? ?™?˜™? ?™?˜™ ? ?™?˜™ ? ?Œ?™?˜™? ?‹¹?Œ?˜™.
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
