using UnityEngine;

public class LockController : MonoBehaviour
{
    public float interactionDistance = 2f;
    public Color clickableColor = Color.red;
    public string itemName = "Lock"; // Name of the item to display in the message UI
    public LockUI lockUI;
    public GameManager gameManager;
    public int locknum;

    private SpriteRenderer itemSprite;

    private void Awake()
    {
        itemSprite = GetComponent<SpriteRenderer>();
    }

    public bool InRange()
    {
        PlayerController player = FindObjectOfType<PlayerController>();
        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
        return (distanceToPlayer <= interactionDistance);
    }

    private void OnMouseDown()
{
    if (InRange())
    {
        lockUI.SetGameManager(gameManager); // Set the GameManager reference in LockUI
        lockUI.DisplayMessage("Enter the password for " + itemName + ":", gameManager.passwords[locknum], locknum);
    }
}


    private void Update()
    {
        if (InRange())
        {
            itemSprite.color = clickableColor;
        }
        else
        {
            itemSprite.color = Color.white;
        }
    }
}
