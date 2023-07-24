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
    private int mission = 0; //수행한 미션 개수
    public string password = "";
    [HideInInspector] public bool isGameOver = false;

    public IntroUI introUI;

    void Awake(){ // start 전에 시행
        if(instance==null){
            instance = this;
        }
    }
    
    private void Start(){
        //StartCoroutine(introUI.ShowIntroSequence());
        password = Random.Range(1000, 10000).ToString();
    }

    private void Update(){
        
    }

    public void MissionDone(){
        mission ++;
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
