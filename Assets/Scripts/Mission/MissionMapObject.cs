using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionMapObject : MonoBehaviour
{
    protected SpriteRenderer spriteRenderer;
    private bool isClickable = false;

    [SerializeField]
    private GameObject missionUI;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<PlayerController>();
        if (player != null)
        {
            spriteRenderer.color = Color.green;
            isClickable = true;
        }
    }

    protected void OnTriggerExit2D(Collider2D collision)
    {
        var player = collision.GetComponent <PlayerController>();
        if(player != null)
        {
            spriteRenderer.color = Color.white;
            isClickable = false;
        }
    }

    private void OnMouseDown()
    {
        if(isClickable)
        {
           missionUI.GetComponent<MissionUI>().Open();
        }
    }
}
