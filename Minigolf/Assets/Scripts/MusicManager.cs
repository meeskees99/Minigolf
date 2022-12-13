using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] AudioSource backgroundMusic;

    private void Start()
    {
        UpdateMusicVolume();
    }

    public void UpdateMusicVolume()
    {
        backgroundMusic.volume = Buttons.musicVolume;
    }
}
