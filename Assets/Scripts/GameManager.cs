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
    public static List<string> passwords = new List<string>();
    [SerializeField]  public List<GameObject> items = new List<GameObject>();
    [HideInInspector] public bool isGameOver = false;

    public IntroUI introUI;

    void Awake(){ // start 전에 시행
        if(instance==null){
            instance = this;
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
    }

    private void Update(){
        
    }

    public void MissionDone(int number){
        mission ++;
        items[number].SetActive(false);
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
