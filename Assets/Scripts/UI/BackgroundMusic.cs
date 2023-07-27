using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void StopBackgroundMusic()
    {
        if (gameObject != null)
        {
            gameObject.GetComponent<AudioSource>().Stop();
        }
    }
}
