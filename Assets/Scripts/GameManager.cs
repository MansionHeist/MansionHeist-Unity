using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    //[SerializeField] private TextMeshProUGUI text; //미션게이지바로 대체
    //[SerializeField] private GameObject gameOverPanel; //게임오버 나타내는 창
    public List<GameObject> Missions = new List<GameObject>();
    private int mission = 0; //수행한 미션 개수
    public List<string> passwords = new List<string>();
    [HideInInspector] public bool isGameOver = false;

    public IntroUI introUI;

    void Awake(){ // start 전에 시행
        if(instance==null){
            instance = this;
        }
    }

    private void GeneratePasswords()
    {
        for (int i = 0; i < 2; i++)
        {
            string password = Random.Range(1000, 10000).ToString();
            passwords.Add(password);
        }
    }
    
    private void Start(){
        //StartCoroutine(introUI.ShowIntroSequence());
        GeneratePasswords();
    }

    private void Update(){
        
    }


    public void HandleItemDisappear(int locknum)
    {
        Missions[locknum].SetActive(false);
        mission++;
    }

    /*
    public void SetGameOver(){
        isGameOver = true;
        Invoke("ShowGameOverPanel",1f); //일정시간 후 수행
    }

    void ShowGameOverPanel(){
        gameOverPanel.SetActive(true);
    }

    public void Exit(){
        SceneManager.LoadScene("MainScene");
    }
*/
}
