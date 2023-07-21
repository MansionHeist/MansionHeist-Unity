using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomEnterButton : MonoBehaviour
{
    public void OnClickEnterRoom()
    {
        SceneManager.LoadScene("RoomScene");
    }
}
