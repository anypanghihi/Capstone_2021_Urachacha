using System.Collections;
using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using UnityEngine.UI;

public class PlayFabManager : MonoBehaviour
{
    public InputField IdInput, PasswordInput, PlayernameInput;

    public string playerName;

    private void Start() 
    {
        playerName = PlayernameInput.text;
    }

    public void LoginButton()
    {
        var request = new LoginWithEmailAddressRequest {Email = IdInput.text, Password = PasswordInput.text};
        PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnLoginFailure);
    }

    public void RegisterButton()
    {
        var request = new RegisterPlayFabUserRequest {Email = IdInput.text, Password = PasswordInput.text, Username = playerName};
        PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterSuccess, OnRegisterFailure);
    }

    private void OnLoginSuccess(LoginResult result)
    {
        Debug.Log("로그인 성공");
    }

    private void OnLoginFailure(PlayFabError error)
    {
        Debug.Log("로그인 실패");
    }

    private void OnRegisterSuccess(RegisterPlayFabUserResult result)
    {
        Debug.Log("회원가입 성공");
    }

    private void OnRegisterFailure(PlayFabError error)
    {
        Debug.Log("회원가입 실패");
    }
}