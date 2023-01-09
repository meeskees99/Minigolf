using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;

public class Playfab : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var request = new LoginWithCustomIDRequest
        {
            CustomId = SystemInfo.deviceUniqueIdentifier,
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
        Debug.Log($"Logged in with ID {result.PlayFabId}");
    }

    void OnError(PlayFabError error)
    {
        Debug.LogError($"Error while trying to login to Playfab: {error.ErrorMessage}");
        Debug.LogWarning("Disabling all playfab features and going to menu..");
    }
}
