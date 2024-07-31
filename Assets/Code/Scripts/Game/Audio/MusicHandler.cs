using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VUDK.Features.Main.AudioSystem;

public class MusicHandler : MonoBehaviour
{
    PSAAudioManager audioManager;

    private void Start()
    {
        audioManager = FindObjectOfType<PSAAudioManager>();
        audioManager.PlayBackgroundMusic();
    }

}
