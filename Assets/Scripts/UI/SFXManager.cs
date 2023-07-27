using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    [SerializeField] private AudioClip audioSFX;

    void WarningAlert()
    {
        AudioSource.PlayClipAtPoint(audioSFX, Camera.main.transform.position);
    }
}
