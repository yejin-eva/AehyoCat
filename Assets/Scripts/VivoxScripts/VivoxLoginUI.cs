using System;
using Unity.Services.Vivox;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.EventSystems;

public class VivoxLoginUI : MonoBehaviour
{
    [SerializeField] private Cat cat;
    [SerializeField] private UnityEngine.UI.Button loginButton;
    [SerializeField] private GameObject LoginScreen;

    private EventSystem eventSystem => FindObjectOfType<EventSystem>();
    private int permissionAskedCount;

    private void Start()
    {
        VivoxService.Instance.LoggedIn += OnUserLoggedIn;
        VivoxService.Instance.LoggedOut += OnUserLoggedOut;

#if !(UNITY_STANDALONE || UNITY_IOS || UNITY_ANDROID)
        DisplayNameInput.interactable = false;
#endif

        loginButton.onClick.AddListener(() => { LoginToVivoxService(); });

        OnUserLoggedOut();
    }

    private void OnDestroy()
    {
        VivoxService.Instance.LoggedIn -= OnUserLoggedIn;
        VivoxService.Instance.LoggedOut -= OnUserLoggedOut;

        loginButton.onClick.RemoveAllListeners();
    }

    bool IsMicPermissionGranted()
    {
        bool isGranted = Permission.HasUserAuthorizedPermission(Permission.Microphone);
        return isGranted;
    }

    private void AskForPermissions()
    {
        string permissionCode = Permission.Microphone;

        permissionAskedCount++;
        Permission.RequestUserPermission(permissionCode);
    }

    private bool IsPermissionDenied()
    {
        return permissionAskedCount == 1;
    }

    private void LoginToVivoxService()
    {
        if (IsMicPermissionGranted())
        {
            LoginToVivox();
        }
        else
        {
            if (IsPermissionDenied())
            {
                permissionAskedCount = 0;
                LoginToVivox();
            }
            else
            {
                AskForPermissions();
            }
        }
    }

    private async void LoginToVivox()
    {
        loginButton.interactable = false;

        await VivoxManager.Instance.InitializeAsync(cat.CatName);
        var loginOptions = new LoginOptions() 
        { 
            DisplayName = cat.CatName,
            ParticipantUpdateFrequency = ParticipantPropertyUpdateFrequency.FivePerSecond
        };
        await VivoxService.Instance.LoginAsync(loginOptions);
    }

    private void ShowLoginUI()
    {
        LoginScreen.SetActive(true);
        loginButton.interactable = true;
    }
    private void HideLoginUI()
    {
        LoginScreen.SetActive(false);
    }

    private void OnUserLoggedOut()
    {
        ShowLoginUI();
    }

    

    private void OnUserLoggedIn()
    {
        HideLoginUI();
    }

    
}
