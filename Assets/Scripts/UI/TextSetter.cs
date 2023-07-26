using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextSetter : MonoBehaviour
{
    TMP_Text tmpText;

    // Start is called before the first frame update
    void Start()
    {
        tmpText = GetComponent<TMP_Text>();
    }

    public void SetGameOverText(string text)
    {
        tmpText.text = text;
    }
}
