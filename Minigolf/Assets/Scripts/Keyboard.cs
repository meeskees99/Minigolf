using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.SceneManagement;

public class Keyboard : MonoBehaviour
{
    [SerializeField] string capCharacter;
    [SerializeField] GameObject buttonName;
    [SerializeField] Image shiftButton;
    [SerializeField] Image capsButton;
    [SerializeField] TextMeshProUGUI displayName;
    [SerializeField] TextMeshProUGUI inputName;
    [SerializeField] TextMeshProUGUI errorMessage;
    static bool capitalize;
    static bool capitalizeOnce;

    private void Update()
    {
        if (capitalize)
        {
            displayName.text = capCharacter;
        }
        else
        {
            displayName.text = buttonName.name;
        }
    }

    public void RegisterCharacter()
    {
        if (Playfab.username.Length < 11)
        {
            Playfab.username += displayName.text;
            inputName.text = $"{Playfab.username}_";
            errorMessage.text = $"";
        }
        else if (Playfab.username.Length == 11)
        {
            Playfab.username += displayName.text;
            inputName.text = $"{Playfab.username}";
            errorMessage.text = $"";
        }
        else
        {
            errorMessage.text = $"Username is too long! (Max 12 characters!)";
        }

        if (capitalizeOnce)
        {
            capitalize = false;
            capitalizeOnce = false;
            shiftButton.color = new Color32(255, 255, 255, 255);
        }
        Debug.Log(Playfab.username);
    }

    public void BackspaceButton()
    {
        Playfab.username = "";
        inputName.text = $"{Playfab.username}_";
        errorMessage.text = $"";
        capitalize = false;
        capitalizeOnce = false;
        shiftButton.color = new Color32(255, 255, 255, 255);
    }

    public void CaptializeOnce()
    {
        capitalize = true;
        capitalizeOnce = true;
        capsButton.color = new Color32(255, 255, 255, 255);
        shiftButton.color = new Color32(255, 255, 150, 255);
    }

    public void Captialize()
    {
        if (!capitalize && !capitalizeOnce)
        {
            capitalize = true;
            capitalizeOnce = false;
            capsButton.color = new Color32(255, 255, 150, 255);
            shiftButton.color = new Color32(255, 255, 255, 255);
        }
        else if (capitalize && capitalizeOnce)
        {
            capitalize = true;
            capitalizeOnce = false;
            capsButton.color = new Color32(255, 255, 150, 255);
            shiftButton.color = new Color32(255, 255, 255, 255);
        }
        else
        {
            capitalize = false;
            capitalizeOnce = false;
            capsButton.color = new Color32(255, 255, 255, 255);
            shiftButton.color = new Color32(255, 255, 255, 255);
        }
    }

    public void Submit()
    {
        if (Playfab.username.Length >= 6 && Playfab.username.Length <= 12)
        {
            var request = new UpdateUserTitleDisplayNameRequest
            {
                DisplayName = $"{Playfab.username}"
            };
            PlayFabClientAPI.UpdateUserTitleDisplayName(request, OnDisplayNameUpdate, OnError);
        }
        else if (Playfab.username.Length < 6)
        {
            errorMessage.text = "Username is too short! (Min 6 characters)";
        }
    }

    void OnDisplayNameUpdate(UpdateUserTitleDisplayNameResult result)
    {
        Debug.Log($"Username set to {Playfab.username}");
        SceneManager.LoadScene("MainMenu");
    }

    void OnError(PlayFabError error)
    {
        Debug.LogError("Error while logging in/creating account!");
        Debug.LogError(error.GenerateErrorReport());
        errorMessage.text = error.ErrorMessage;
    }
}
