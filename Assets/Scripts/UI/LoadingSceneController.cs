using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class LoadingSceneController : MonoBehaviour
{
    static string nextScene;

    [SerializeField]
    Image progressBar;

    public void loadScene(string sceneName)
    {
        nextScene = sceneName;
        StartCoroutine(LoadSceneProcess());
        //SceneManager.LoadScene("LoadingScene");
    }

    private class UserInfo{
        public string userType;
        public List<string> userNameList;
        public List<string> userTypeList;
        public int userRoomIdx;
    }

    public void setUserInfo(string data){
        Debug.Log("setUserInfo: " + data);
        UserInfo userInfo = JsonUtility.FromJson<UserInfo>(data);
        Debug.Log("userType: " + userInfo.userType);
        if(userInfo.userType=="theif"){
            PlayerSettings.userType = EPlayerType.Thief;
        }else{
            PlayerSettings.userType = EPlayerType.Guard;
        }
        ServerManager serverManager = GameObject.Find("ServerManager").GetComponent<ServerManager>();
        serverManager.setUserRoomIdx(userInfo.userRoomIdx);
        PlayerManager playerManager = GameObject.Find("PlayerManager").GetComponent<PlayerManager>();
        playerManager.initPlayerList(userInfo.userNameList, userInfo.userTypeList);

        // FINISH SETTING
        serverManager.emitMessage("loading/user-loaded", "");
    }

    // Start is called before the first frame update
    void Start()
    {
        ServerManager serverManager = GameObject.Find("ServerManager").GetComponent<ServerManager>();
        serverManager.emitMessage("loading/get-user-info", "");
        nextScene = "";
    }

    IEnumerator LoadSceneProcess()
    {
        while(nextScene==""){
            
        }
        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);
        op.allowSceneActivation = false;

        float timer = 0f;
        while(!op.isDone)
        {
            yield return null;

            if(op.progress < 0.9f)
            {
                progressBar.fillAmount = op.progress;
            } 
            else
            {
                timer += Time.unscaledDeltaTime;
                progressBar.fillAmount = Mathf.Lerp(0.9f, 1f, timer);
                if(progressBar.fillAmount >= 1f)
                {
                    op.allowSceneActivation = true;
                    yield break;
                }
            }
        }
    }
}
