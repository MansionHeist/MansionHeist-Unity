using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LongPress : MonoBehaviour
{
    private bool isClick;
    private float clickedTime;

    [SerializeField]
    private float minClickTime = 4f;

    [SerializeField]
    private MissionUI missionUI;
    
    public void ButtonDown()
    {
        isClick = true;
    }

    public void ButtonUp()
    {
        isClick = false;
        if(clickedTime >= minClickTime)
        {
            // TODO: Ư�� ��� ����
            // print("���� �ð�: " + clickedTime.ToString());
            clickedTime = 0;
            missionUI.SuccessClose(4);
        }
        else
        {
            clickedTime = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isClick)
        {
            clickedTime += Time.deltaTime;
        }
    }
}
