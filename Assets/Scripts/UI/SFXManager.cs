using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    [SerializeField] private AudioClip audio;

    void WarningAlert()
    {
        AudioSource.PlayClipAtPoint(audio, Camera.main.transform.position);
    }
}
