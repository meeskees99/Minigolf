using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    [SerializeField] Image dimmedBrightnessImage;

    private void Update()
    {
        var tempColor = dimmedBrightnessImage.color;
        float brightness = Buttons.brightness;
        tempColor.a = 1 - brightness / 100;
        dimmedBrightnessImage.color = tempColor;
    }
}
