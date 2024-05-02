using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Services.Authentication;
using Unity.Services.Vivox;
using UnityEngine;
using UnityEngine.EventSystems;

public class VivoxLobbyUI : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Button logoutButton;
    [SerializeField] private UnityEngine.UI.Button lobbyScreenIcon;
    [SerializeField] private GameObject lobbyScreen;
    [SerializeField] private GameObject loggedInUsersUI;
    [SerializeField] private GameObject vivoxUserLoginPrefab;
    [SerializeField] private GameObject connectionIndicatorDot;
    [SerializeField] private GameObject connectionIndicatorText;

    private EventSystem eventSystem;
    private GameObject connectedUserStatusObject = null;
    private UnityEngine.UI.Image connectionIndicatorDotImage;
    private TMPro.TextMeshProUGUI connectionIndicatorDotText;
    private Dictionary<string, GameObject> connectedUsers = new Dictionary<string, GameObject>();

    private void Start()
    {
        eventSystem = EventSystem.current;
        if (!eventSystem)
        {
            Debug.LogError("Unable to find EventSystem object.");
        }
        connectionIndicatorDotImage = connectionIndicatorDot.GetComponent<UnityEngine.UI.Image>();
        if (!connectionIndicatorDotImage)
        {
            Debug.LogError("Unable to find ConnectionIndicatorDot Image object.");
        }
        connectionIndicatorDotText = connectionIndicatorText.GetComponent<TMPro.TextMeshProUGUI>();
        if (!connectionIndicatorDotText)
        {
            Debug.LogError("Unable to find ConnectionIndicatorText Text object.");
        }

        VivoxService.Instance.LoggedIn += OnUserLoggedIn;
        VivoxService.Instance.LoggedOut += OnUserLoggedOut;
        VivoxService.Instance.ParticipantAddedToChannel += OnParticipantAddedToChannel;
        VivoxService.Instance.ParticipantRemovedFromChannel += OnParticipantRemovedFromChannel;
        VivoxService.Instance.ConnectionRecovered += OnConnectionRecovered;
        VivoxService.Instance.ConnectionRecovering += OnConnectionRecovering;
        VivoxService.Instance.ConnectionFailedToRecover += OnConnectionFailedToRecover;

        connectionIndicatorDotImage.color = Color.green;
        connectionIndicatorDotText.text = "Connected";

        logoutButton.onClick.AddListener(() => { LogoutOfVivoxServiceAsync(); });
        lobbyScreenIcon.onClick.AddListener(() => { OnLobbyScreenIconClicked(); });

        lobbyScreenIcon.gameObject.SetActive(false);
        // Make sure the UI is in a reset/off state from the start.
        OnUserLoggedOut();
    }

    private void OnParticipantRemovedFromChannel(VivoxParticipant participant)
    {
        DestroyConnectedUser(participant);
    }

    private void OnParticipantAddedToChannel(VivoxParticipant participant)
    {
        CreateConnectedUser(participant);
    }

    private void OnDestroy()
    {
        VivoxService.Instance.LoggedIn -= OnUserLoggedIn;
        VivoxService.Instance.LoggedOut -= OnUserLoggedOut;
        VivoxService.Instance.ParticipantAddedToChannel -= OnParticipantAddedToChannel;
        VivoxService.Instance.ParticipantRemovedFromChannel -= OnParticipantRemovedFromChannel;
        VivoxService.Instance.ConnectionRecovered -= OnConnectionRecovered;
        VivoxService.Instance.ConnectionRecovering -= OnConnectionRecovering;
        VivoxService.Instance.ConnectionFailedToRecover -= OnConnectionFailedToRecover;

        logoutButton.onClick.RemoveAllListeners();
    }
    Task JoinLobbyChannel()
    {
        return VivoxService.Instance.JoinGroupChannelAsync(VivoxManager.LobbyChannelName, ChatCapability.TextAndAudio);
    }
    private async void LogoutOfVivoxServiceAsync()
    {
        logoutButton.interactable = false;

        await VivoxService.Instance.LogoutAsync();
        AuthenticationService.Instance.SignOut();
    }

    private async void OnUserLoggedIn()
    {
        lobbyScreen.SetActive(true);
        await JoinLobbyChannel();
        logoutButton.interactable = true;

        lobbyScreenIcon.gameObject.SetActive(true);

        eventSystem.SetSelectedGameObject(logoutButton.gameObject, null);
    }

    private void OnUserLoggedOut()
    {
        lobbyScreen.SetActive(false);
        lobbyScreenIcon.gameObject.SetActive(false);
        
    }
    private void CreateConnectedUser(VivoxParticipant participant)
    {
        var connectedUser = Instantiate(vivoxUserLoginPrefab, loggedInUsersUI.transform);
        var vivoxUserLogin = connectedUser.GetComponent<vivoxUserLoginPrefab>();
        vivoxUserLogin.SetUserId(participant.PlayerId);
        vivoxUserLogin.SetDisplayName(participant.DisplayName);

        connectedUsers.Add(participant.PlayerId, connectedUser);
    }
    private void DestroyConnectedUser(VivoxParticipant participant)
    {
        if (connectedUsers.TryGetValue(participant.PlayerId, out GameObject userGameObject))
        {
            connectedUsers.Remove(participant.PlayerId);
            Destroy(userGameObject);
        }
        else
        {
            Debug.Log("User not found: " + participant.DisplayName);
        }
    }

    private void OnLobbyScreenIconClicked()
    {
        if (lobbyScreen.activeSelf == false)
        {
            lobbyScreen.SetActive(true);
        }
        else
        {
            lobbyScreen.SetActive(false);
        }
    }
    private void OnConnectionRecovering()
    {
        connectionIndicatorDotImage.color = Color.yellow;
        connectionIndicatorDotText.text = "Connection Recovering";
    }
    private void OnConnectionRecovered()
    {
        connectionIndicatorDotImage.color = Color.green;
        connectionIndicatorDotText.text = "Connection Recovered";
    }

    private void OnConnectionFailedToRecover()
    {
        connectionIndicatorDotImage.color = Color.red;
        connectionIndicatorDotText.text = "Connection Failed to Recover";
    }
}
