using UnityEngine;
using UnityEngine.UI;

public class ClosetPW : MonoBehaviour
{

    public Text message;
    public int locknum;


    private void Start()
    {
        message.text = GameManager.passwords[locknum];    
    }
}
