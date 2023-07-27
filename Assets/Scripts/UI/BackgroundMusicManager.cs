using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackgroundMusicManager : MonoBehaviour
{
    [SerializeField] private AudioClip MainUIMusic;
    [SerializeField] private AudioClip PlayMusic;

    // Start is called before the first frame update
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name.Equals("MansionMap"))
        {
            AudioSource audioSource = gameObject.GetComponent<AudioSource>();
            audioSource.Stop();
            audioSource.clip = PlayMusic;
            audioSource.Play();
        }
    }
}
