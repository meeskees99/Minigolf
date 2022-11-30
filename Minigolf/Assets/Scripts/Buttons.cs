using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Buttons : MonoBehaviour
{
    [Header("LoadingScreen")]
    [SerializeField] Image[] dimmedImages;
    [SerializeField] float dimSpeed;
    [SerializeField] float loadTime;

    public void GoToScene(string sceneName)
    {
        StartCoroutine(Transition(sceneName));
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
