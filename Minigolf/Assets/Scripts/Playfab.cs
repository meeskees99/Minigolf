using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.SceneManagement;

public class Playfab : MonoBehaviour
{
    static public bool loggedIn;
    static public string username = "";
    static public string id;
    [SerializeField] TextMeshProUGUI inputName;
    [SerializeField] TextMeshProUGUI errorMessage;
    [SerializeField] Buttons loader;

    // Start is called before the first frame update
    void Start()
    {
        inputName.text = $"Logging in..";
        errorMessage.text = $"";
        Debug.Log($"Trying to log in as {SystemInfo.deviceUniqueIdentifier}");
        var request = new LoginWithCustomIDRequest
        {
            CustomId = $"DEVICE{SystemInfo.deviceUniqueIdentifier}",
            CreateAccount = true,
            InfoRequestParameters = new GetPlayerCombinedInfoRequestParams
            {
                GetPlayerProfile = true
            }
        };
        PlayFabClientAPI.LoginWithCustomID(request, OnLoginSucces, OnError);
    }


    void OnLoginSucces(LoginResult result)
    {
        Debug.Log("Logged in!");
        string name = null;
        id = result.PlayFabId;
        if (result.InfoResultPayload.PlayerProfile != null)
        {
            name = result.InfoResultPayload.PlayerProfile.DisplayName;
        }

        if (name == null)
        {
            username = "";
            inputName.text = $"{Playfab.username}_";
        }
        else
        {
            username = name;
            loader.GoToScene("MainMenu");
        }
    }

    void OnError(PlayFabError error)
    {
        Debug.LogError($"Error while trying to login to Playfab: {error.ErrorMessage}");
        Debug.LogWarning("Disabling all playfab features and going to menu..");
        loggedIn = false;
    }
}
