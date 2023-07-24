using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionUI : MonoBehaviour
{
    private GameObject missionUI;
    void Start()
    {
        missionUI = GetComponent<GameObject>();
    }

    public void Open()
    {
        PlayerController.isMoveable = false;
        missionUI.transform.parent.gameObject.SetActive(true);
        gameObject.SetActive(true);
    }

    public void Close()
    {
        PlayerController.isMoveable = true;
        missionUI.transform.parent.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }
}
