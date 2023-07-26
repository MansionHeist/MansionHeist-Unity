using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinimapUI : MonoBehaviour
{

    [SerializeField]
    private Transform left;
    [SerializeField]
    private Transform right;
    [SerializeField]
    private Transform top;
    [SerializeField]
    private Transform bottom;
    [SerializeField]
    private Image minimapImage;
    [SerializeField]
    private Image minimapPlayerImage;

    [SerializeField] private PlayerController targetPlayer;
    // Start is called before the first frame update
    void Start()
    {
        var inst = Instantiate(minimapImage.material);
        minimapImage.material = inst;
    }

    // Update is called once per frame
    void Update()
    {
        if (targetPlayer != null)
        {
            Vector2 mapArea = new Vector2(Vector3.Distance(left.position, right.position), Vector3.Distance(bottom.position, top.position));
            // Calculate the distance of the character's position from the left edge of the minimap
            float xDistanceFromLeft = Vector3.Distance(new Vector3(left.position.x, 0f, 0f), new Vector3(targetPlayer.transform.position.x, 0f, 0f));
            // Calculate the distance of the character's position from the bottom edge of the minimap
            float yDistanceFromBottom = Vector3.Distance(new Vector3(0f, bottom.position.y, 0f), new Vector3(0f, targetPlayer.transform.position.y, 0f));

            // Clamp the x and y positions within the range of the minimap's left, right, top, and bottom edges
            //float clampedX = Mathf.Clamp(xDistanceFromLeft, 0f, mapArea.x);
            //float clampedY = Mathf.Clamp(yDistanceFromBottom, 0f, mapArea.y);

            // Calculate the normalized position of the character within the minimap
            Vector2 normalPos = new Vector2(xDistanceFromLeft / mapArea.x, yDistanceFromBottom / mapArea.y);
            minimapPlayerImage.rectTransform.anchoredPosition = new Vector2(minimapImage.rectTransform.sizeDelta.x * normalPos.x, minimapImage.rectTransform.sizeDelta.y * normalPos.y);
           
        }
    }



    public void Open(){
        gameObject.SetActive(true);
    }
    public void Close(){
        gameObject.SetActive(false);
    }
}
