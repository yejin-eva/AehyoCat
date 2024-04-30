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
    [SerializeField] private GameObject loginStatus;
    [SerializeField] private TMPro.TextMeshProUGUI loginStatusText;

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

        OnUserLoggedIn();
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
        loginStatus.SetActive(true);
        loginStatusText.text = "Logging in...";

        loginButton.interactable = false;

        await VivoxManager.Instance.InitializeAsync(cat.CatName);
        var loginOptions = new LoginOptions() 
        { 
            DisplayName = $"{cat.CatName}'s owner",
            ParticipantUpdateFrequency = ParticipantPropertyUpdateFrequency.FivePerSecond
        };

        try
        {
            await VivoxService.Instance.LoginAsync(loginOptions);
            loginStatusText.text = "";
        }
        catch (Exception e)
        {
            loginStatusText.text = "Login failed";
            Debug.LogError(e);
        }
    }

    private void ShowLoginUI()
    {
        LoginScreen.SetActive(true);
        loginButton.interactable = true;
    }
    private void HideLoginUI()
    {
        LoginScreen.SetActive(false);
        loginStatus.SetActive(false);
    }

    public void OnUserLoggedOut()
    {
        ShowLoginUI();
    }

    

    private void OnUserLoggedIn()
    {
        HideLoginUI();
    }

    
}
