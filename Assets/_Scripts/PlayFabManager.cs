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
        var request = new LoginWithEmailAddressRequest 
        {
            Email = IdInput.text, Password = PasswordInput.text
        };

        PlayFabClientAPI.LoginWithEmailAddress(request, 
        (result) => 
            { 
                // playerName님 환영합니다 와 같은 문구 출력
                print("로그인 성공");
            }, 
        (error) => 
            {
                print("로그인 실패");
            }
        );
    }

    public void RegisterButton()
    {
        var request = new RegisterPlayFabUserRequest 
        {
            Email = IdInput.text, Password = PasswordInput.text, Username = playerName
        };

        PlayFabClientAPI.RegisterPlayFabUser(request, 
        (result) => 
            { 
                print("회원가입 성공");
            }, 
        (error) => 
            {
                print("회원가입 실패");
            }
        );
    }

    public void SetNickname()
    {
        var request = new UpdateUserDataRequest()
        { 
            Data = new Dictionary<string, string>() 
            {
                {"Nickname", playerName} 
            } 
        };

        PlayFabClientAPI.UpdateUserData(request, 
        (result) => 
            {
                print("데이터 저장 성공");
            }, 
        (error) => 
            {
                print("데이터 저장 실패");
            }
        );
    }

    public void GetNickname()
    {
        var request = new GetUserDataRequest() 
        {

        };

        PlayFabClientAPI.GetUserData(request, 
        (result) => 
            {
                foreach (var eachData in result.Data)
                {
                    playerName = eachData.Value.Value;
                }
            }, 
        (error) => 
            {
                print("데이터 불러오기 실패");
            }
        );
    }
}