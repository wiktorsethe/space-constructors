using System;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.SavedGame;
using UnityEngine.UI;
using TMPro;

public class GoogleIntegration : MonoBehaviour
{
    private InputField dataToCloud;

    private void Start()
    {
        SignUserWithPlayGames();
    }
    void SignUserWithPlayGames()
    {
        PlayGamesPlatform.Instance.Authenticate(SuccessCallback);
    }
    internal void SuccessCallback(SignInStatus success)
    {
        if(success == SignInStatus.Success)
        {
            Debug.Log("Signined in player using Play Services");
        }
        else if(success == SignInStatus.InternalError)
        {
            Debug.Log("Internal Error");
        }
        else if(success == SignInStatus.Canceled)
        {
            Debug.Log("Signin not successfull");
        }
    }
}
