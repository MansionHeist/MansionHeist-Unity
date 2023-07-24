using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    public float interactionDistance = 2f;
    public Color clickableColor = Color.red;
    public MessageUIController messageUIController;
    public GameManager gameManager;

    private SpriteRenderer itemSprite;

    private void Awake(){
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
            messageUIController.DisplayMessage("Closet Lock Password: " + gameManager.password);
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
