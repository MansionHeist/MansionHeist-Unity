using UnityEngine;

public class LockController : MonoBehaviour
{
    public float interactionDistance = 2f;
    public Color clickableColor = Color.red;
    public string itemName = "Closet Lock"; // Name of the item to display in the message UI
    public ItemController itemController;
    public LockUI lockUI;
    public GameManager gameManager;

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
            // Display the message UI with the text input and the correct password
            lockUI.DisplayMessage("Enter the password for " + itemName + ":", gameManager.password);
        }
    }

    public void HandleItemDisappear()
    {
        // Handle item disappearance here (e.g., destroy the item GameObject)
        gameObject.SetActive(false);
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
