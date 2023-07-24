using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionUI : MonoBehaviour
{
    void Start()
    {
    }

    public void Open()
    {
        PlayerController.isMoveable = false;
        gameObject.transform.parent.gameObject.SetActive(true);
        gameObject.SetActive(true);
    }

    public void Close()
    {
        PlayerController.isMoveable = true;
        gameObject.transform.parent.gameObject.SetActive(false);
    }
}
