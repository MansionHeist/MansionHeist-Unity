using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragObject : MonoBehaviour, IDragHandler
{
    //[SerializeField]
    //private float maxXBound = 100f;
    //[SerializeField]
    //private float maxYBound = 100f;

    public void OnDrag(PointerEventData eventData)
    {
        //Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //float xpos = Mathf.Clamp(mousePos.x, -maxXBound, maxXBound); 
        //float ypos = Mathf.Clamp(mousePos.y, -maxYBound, maxYBound);

        //Vector3 mousePosition = new Vector3(xpos, ypos, gameObject.transform.position.z);
        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, gameObject.transform.position.z);
        transform.position = mousePosition;
    }
}
