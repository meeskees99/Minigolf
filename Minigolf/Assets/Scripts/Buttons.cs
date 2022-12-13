using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System;

public class Buttons : MonoBehaviour
{
    [Header("LoadingScreen")]
    [SerializeField] Image[] dimmedImages;
    [SerializeField] float dimSpeed;
    [SerializeField] float loadTime;
    [Space(20)]
    [Header("Options")]
    [SerializeField] Settings settingManager;
    [SerializeField] MusicManager backgroundMusic;
    static public bool walking = true;
    static public bool snapturning = false;
    static public int brightness = 100;
    static public float volume = 1;
    static public float musicVolume = 1;
    static public float playerHeightOffset = 0f;

    public void GoToScene(string sceneName)
    {
        StartCoroutine(Transition(sceneName));
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("The application would have been closed, but that doesn't work in the editor!");
    }

    public void ToggleWalkTp(TextMeshProUGUI buttonText)
    {
        walking = !walking;
        if (walking)
        {
            buttonText.text = "  < Walk >";
        }
        else
        {
            buttonText.text = "  < Teleport >";
        }
        settingManager.UpdateSettings();
    }

    public void ToggleSnapturning(TextMeshProUGUI buttonText)
    {
        snapturning = !snapturning;
        if (snapturning)
        {
            buttonText.text = "  < Snapturning: On >";
        }
        else
        {
            buttonText.text = "  < Snapturning: Off >";
        }
        settingManager.UpdateSettings();
    }

    public void ChangeBrightnessUp(TextMeshProUGUI buttonText)
    {
        if (brightness <= 90)
        {
            brightness += 10;
        }
        buttonText.text = $"  < Brightness >       {brightness}";
        settingManager.UpdateSettings();
    }

    public void ChangeBrightnessDown(TextMeshProUGUI buttonText)
    {
        if (brightness >= 20)
        {
            brightness -= 10;
        }
        buttonText.text = $"  < Brightness >       {brightness}";
        settingManager.UpdateSettings();
    }

    public void ChangeVolumeUp(TextMeshProUGUI buttonText)
    {
        if (volume <= .9f)
        {
            volume += .1f;
            volume = (float)Math.Round(volume, 1);
        }
        buttonText.text = $"  < Global Volume >     {volume * 100}";
        settingManager.UpdateSettings();
    }

    public void ChangeVolumeDown(TextMeshProUGUI buttonText)
    {
        if (volume >= .1f)
        {
            volume -= .1f;
            volume = (float)Math.Round(volume, 1);
        }
        buttonText.text = $"  < Global Volume >     {volume * 100}";
        settingManager.UpdateSettings();
    }

    public void ChangeMusicVolumeUp(TextMeshProUGUI buttonText)
    {
        if (musicVolume <= .9f)
        {
            musicVolume += .1f;
            musicVolume = (float)Math.Round(musicVolume, 1);
        }
        buttonText.text = $"  < Music Volume >     {musicVolume * 100}";
        backgroundMusic.UpdateMusicVolume();
    }

    public void ChangeMusicVolumeDown(TextMeshProUGUI buttonText)
    {
        if (musicVolume >= .1f)
        {
            musicVolume -= .1f;
            musicVolume = (float)Math.Round(musicVolume, 1);
        }
        buttonText.text = $"  < Music Volume >     {musicVolume * 100}";
        backgroundMusic.UpdateMusicVolume();
    }

    public void ChangePlayerHeightUp(TextMeshProUGUI buttonText)
    {
        if (playerHeightOffset <= .3f)
        {
            playerHeightOffset += .1f;
        }
        float playerHeightDisplay = playerHeightOffset + 1.7f;
        playerHeightDisplay = playerHeightDisplay * 100;
        buttonText.text = $"  < Player Height >     {playerHeightDisplay}";
        settingManager.UpdateSettings();
    }

    public void ChangePlayerHeightDown(TextMeshProUGUI buttonText)
    {
        if (playerHeightOffset >= -0.7f)
        {
            playerHeightOffset -= .1f;
        }
        float playerHeightDisplay = playerHeightOffset + 1.7f;
        playerHeightDisplay = playerHeightDisplay * 100;
        buttonText.text = $"  < Player Height >     {playerHeightDisplay}";
        settingManager.UpdateSettings();
    }

    IEnumerator Transition(string sceneName)
    {
        foreach (Image dimmedImage in dimmedImages)
        {
            var tempColor = dimmedImage.color;
            tempColor.a += dimSpeed * Time.deltaTime;
            dimmedImage.color = tempColor;
        }
        yield return new WaitForEndOfFrame();
        if (dimmedImages[0].color.a < 1)
        {
            StartCoroutine(Transition(sceneName));
        }
        else
        {
            yield return new WaitForSeconds(loadTime);
            SceneManager.LoadScene(sceneName);
        }
    }
}
