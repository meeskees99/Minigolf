using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Buttons : MonoBehaviour
{
    [Header("LoadingScreen")]
    [SerializeField] Image[] dimmedImages;
    [SerializeField] float dimSpeed;
    [SerializeField] float loadTime;
    [Space(20)]
    [Header("Options")]
    static public bool walking = true;
    static public bool snapturning = false;
    static public int brightness = 100;


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
    }

    public void ChangeBrightnessUp(TextMeshProUGUI buttonText)
    {
        if (brightness <= 90)
        {
            brightness += 10;
        }
        buttonText.text = $"  < Brightness >       {brightness}";
    }

    public void ChangeBrightnessDown(TextMeshProUGUI buttonText)
    {
        if (brightness >= 20)
        {
            brightness -= 10;
        }
        buttonText.text = $"  < Brightness >       {brightness}";
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
