using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionBackground : MonoBehaviour
{
    private GameObject background;

    // Start is called before the first frame update
    void OnEnable()
    {
        background = GetComponent<GameObject>();
    }

    void OnMouseDown()
    {
        background.SetActive(false);
    }
}
