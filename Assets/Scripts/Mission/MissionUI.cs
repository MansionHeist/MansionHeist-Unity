using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using static UnityEditor.FilePathAttribute;

public class MissionUI : MonoBehaviour
{
    [SerializeField] public MissionCompleteUI missionCompleteUI;
    [SerializeField] public AlarmUI alarm;

    public void Open()
    {
        PlayerController.StopMoving();
        gameObject.transform.parent.gameObject.SetActive(true);
        gameObject.SetActive(true);
    }

    public void Close()
    {
        PlayerController.isMoveable = true;
        gameObject.SetActive(false);
        gameObject.transform.parent.gameObject.SetActive(false);

    }

    public void SuccessClose(int num)
    {
        /*GameManager gameManager = GameManager.instance;
        gameManager.MissionDone(num);*/
        ServerManager serverManager = GameObject.Find("ServerManager").GetComponent<ServerManager>();
        serverManager.emitMessage("game/mission-done", num.ToString());
        Close();
        missionCompleteUI.Show();
        
    }

    public void FailClose(string location)
    {
        Close();
        alarm.StartFlickering(location);
    }

}
