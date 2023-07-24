using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSceneUI : MonoBehaviour
{
    public void OnClickReadyButton()
    {
        LoadingSceneController.LoadScene("MansionMap");
    }

    public void OnClickStartButton()
    {
        LoadingSceneController.LoadScene("MansionMap");
    }
}
