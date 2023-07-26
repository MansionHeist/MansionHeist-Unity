using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    [SerializeField] private Image progressbar; //미션게이지바로 대체
    //[SerializeField] private GameObject gameOverPanel; //게임오버 나타내는 창
    private int mission = 0; //수행한 미션 개수
    private int total = 11;
    public static List<string> passwords = new List<string>();
    [SerializeField]  public List<GameObject> items = new List<GameObject>();
    [HideInInspector] public bool isGameOver = false;

    public IntroUI introUI;

    [SerializeField] GameObject missionMapObjects;
    [SerializeField] GameObject ThiefUI;
    [SerializeField] GameObject GuardUI;

    void Awake(){ // start 전에 시행
        if(instance==null){
            instance = this;
        }

        if (PlayerSettings.userType == EPlayerType.Thief)
        {
            GuardUI.SetActive(false);
            missionMapObjects.SetActive(true);
            ThiefUI.SetActive(true);
        }
        else if (PlayerSettings.userType == EPlayerType.Guard)
        {
            ThiefUI.SetActive(false);
            missionMapObjects.SetActive(false);
            GuardUI.SetActive(true);
        }
    }

    private static bool ContainsZero(int number)
    {
        while (number > 0)
        {
            int digit = number % 10;
            if (digit == 0)
                return true;

            number /= 10;
        }

        return false;
    }

    private void Start(){

        StartCoroutine(introUI.ShowIntroSequence());
        StartCoroutine(introUI.TimerCoroutine());
       
        int randomNumber = 0;
        for (int i = 0; i < 3; i++){
            do
            {
                randomNumber = Random.Range(1000, 10000);
            } while (ContainsZero(randomNumber));
            passwords.Add(randomNumber.ToString());
        }
        SetProgressBar();
    }

    private void SetProgressBar()
    {
        float progressRatio = (float)mission / total;
        progressbar.fillAmount = progressRatio;
    }

    private void Update(){

    }

    public void MissionDone(int number){
        mission++;

        if (number == 0||number==1) {
            items[number*2].SetActive(false);
            items[number*2+1].SetActive(false);
        }
        else
        {
            items[number].SetActive(false);
        }
        
        SetProgressBar();
        if(mission == total){
            SetGameOver((PlayerSettings.userType == EPlayerType.Thief));
        }
    }

    private bool isWin = false;
    
    public void SetGameOver(bool _isWin){
        isWin = _isWin;
        isGameOver = true;
        ServerManager serverManager = GameObject.Find("ServerManager").GetComponent<ServerManager>();
        serverManager.emitMessage("game/game-over", "");
        Invoke("ShowGameOverPanel",1f); //일정시간 후 수행
    }

    void ShowGameOverPanel(){
        //gameOverPanel.SetActive(true);
        //PlayerManager playerManager = GameObject.Find("PlayerManager").GetComponent<PlayerManager>();
        Destroy(GameObject.Find("PlayerManager"));
        Destroy(GameObject.Find("ServerManager"));
        foreach(GameObject playerObj in GameObject.FindGameObjectsWithTag("CaughtPlayer")){
            Destroy(playerObj);
        }
        foreach(GameObject playerObj in GameObject.FindGameObjectsWithTag("TheifPlayer")){
            Destroy(playerObj);
        }
        foreach(GameObject playerObj in GameObject.FindGameObjectsWithTag("GuardPlayer")){
            Destroy(playerObj);
        }
        GameObject gameOverUI = GameObject.Find("UICanvas").transform.GetChild(3).gameObject;
        gameOverUI.SetActive(true);
        GameObject winUI = gameOverUI.transform.GetChild(1).gameObject;
        GameObject loseUI = gameOverUI.transform.GetChild(2).gameObject;
        if(isWin){
            winUI.SetActive(true);
        }else{
            loseUI.SetActive(true);
        }
        Invoke("Exit", 7f);
    }

    public void Exit(){
        SceneManager.LoadScene("MainScene");
    }
}
