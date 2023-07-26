using UnityEngine;
using UnityEngine.UI;

public class ClosetPW : MonoBehaviour
{

    public static ClosetPW Instance;

    public Text message;
    public int locknum;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        message.text = GameManager.passwords[locknum];    
    }
}
