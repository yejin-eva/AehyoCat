using System;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Vivox;
using UnityEngine;
using System.Threading.Tasks;

[Serializable]
public class Vivox
{
    public Uri server = new Uri(ApiKeys.serverKey);
    public string issuer = ApiKeys.issuer;
    public string domain = ApiKeys.domain;
    public string tokenKey = ApiKeys.tokenKey;
    public TimeSpan timeSpan = TimeSpan.FromSeconds(90);
}

public class VivoxManager : MonoBehaviour
{
    public Vivox vivox = new Vivox();

    static VivoxManager vivoxInstance;

    public static VivoxManager Instance
    {
        get
        {
            if (vivoxInstance == null)
            {
                vivoxInstance = FindObjectOfType<VivoxManager>();

                if (vivoxInstance == null)
                {
                    var vivoxManagerObject = new GameObject();
                    vivoxInstance = vivoxManagerObject.AddComponent<VivoxManager>();
                    vivoxManagerObject.name = typeof(VivoxManager).ToString();
                }
                
            }
            DontDestroyOnLoad(vivoxInstance.gameObject);
            return vivoxInstance;
        }
    }

    private async void Awake()
    {
        if (vivoxInstance != this && vivoxInstance != null)
        {
            Debug.LogWarning("Duplicate VivoxManager detected, destroying this instance");
            Destroy(this);
        }

        var options = new InitializationOptions();
        if (CheckManualCredentials())
        {
            options.SetVivoxCredentials(vivox.server.ToString(), vivox.domain, vivox.issuer, vivox.tokenKey);
        }

        await UnityServices.InitializeAsync(options);
    }

    public async Task InitializeAsync(string playerName)
    {
        if (!CheckManualCredentials())
        {
            AuthenticationService.Instance.SwitchProfile(playerName);
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
        }

        await VivoxService.Instance.InitializeAsync();
    }
    

    bool CheckManualCredentials()
    {
        return !(string.IsNullOrEmpty(vivox.issuer) && string.IsNullOrEmpty(vivox.domain) && string.IsNullOrEmpty(vivox.server.ToString()));
    }
}
