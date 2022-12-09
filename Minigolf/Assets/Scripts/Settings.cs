using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Settings : MonoBehaviour
{
    [SerializeField] Image dimmedBrightnessImage;
    [SerializeField] AudioMixerGroup globalVolume;

    private void Update()
    {
        //brightness
        var tempColor = dimmedBrightnessImage.color;
        float brightness = Buttons.brightness;
        tempColor.a = 1 - brightness / 100;
        dimmedBrightnessImage.color = tempColor;

        //volume
        AudioListener.volume = Buttons.brightness;
    }
}
