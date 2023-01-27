using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using Unity.XR.CoreUtils;

public class Settings : MonoBehaviour
{
    [SerializeField] Image dimmedBrightnessImage;
    [SerializeField] Transform camOffset;

    private void Start()
    {
        UpdateSettings();
    }

    private void Update()
    {
        UpdateSettings();
    }

    public void UpdateSettings()
    {
        //brightness
        var tempColor = dimmedBrightnessImage.color;
        float brightness = Buttons.brightness;
        tempColor.a = 1 - brightness / 100;
        dimmedBrightnessImage.color = tempColor;

        //volume
        AudioListener.volume = Buttons.volume;

        //player height
        //camOffset.position = new Vector3(camOffset.position.x, Buttons.playerHeightOffset + 2.7f, camOffset.position.z);
    }
}
