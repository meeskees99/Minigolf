using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimatedImage : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField] float animationLenght;
    float timer;
    [SerializeField] Sprite[] sprites;
    [SerializeField] Image targetImage;
    int index;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer > animationLenght / sprites.Length - 1)
        {
            index++;
            timer = 0;
        }
        if (index > sprites.Length - 1)
        {
            index = 0;
        }
        targetImage.sprite = sprites[index];
    }
}
