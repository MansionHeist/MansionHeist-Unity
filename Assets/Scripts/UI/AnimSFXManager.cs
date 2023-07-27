using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimSFXManager : MonoBehaviour
{
    [SerializeField] private AudioClip audioSFX;

    void WarningAlert()
    {
        AudioSource.PlayClipAtPoint(audioSFX, Camera.main.transform.position);
    }
}
