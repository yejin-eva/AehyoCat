using System;
using Unity.Services.Vivox;
using UnityEngine;
using UnityEngine.UI;

public class VivoxUserPrefab : MonoBehaviour
{
    public Action<VivoxUserPrefab, bool> onToggledParticipant;
    public Toggle ToggleButton { get => toggleButton; }
    public VivoxParticipant Participant { get => participant; }

    [SerializeField] private TMPro.TextMeshProUGUI displayNameText;
    [SerializeField] private Toggle toggleButton;
    [SerializeField] private Button muteButton;
    [SerializeField] private Sprite muteSprite;
    [SerializeField] private Sprite notSpeakingSprite;
    [SerializeField] private Sprite speakingSprite;

    VivoxParticipant participant;

    bool isMute = false;

    public void SetupVivoxUser(VivoxParticipant participant)
    {
        this.participant = participant;

        if (participant.IsSelf)
        {
            toggleButton.gameObject.SetActive(false);
        }
        else
        {
            toggleButton.gameObject.SetActive(true);
        }
        this.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => OnVivoxUserPrefabButtonClicked());
        toggleButton.onValueChanged.AddListener((value) => OnToggleValueChanged(value));
        muteButton.onClick.AddListener(() => OnMuteButtonClicked());

        participant.ParticipantMuteStateChanged += UpdateChatStateImage;
        participant.ParticipantSpeechDetected += UpdateChatStateImage;

        SetDisplayName(participant.DisplayName);
        muteButton.image.sprite = notSpeakingSprite;
    }

    private void UpdateChatStateImage()
    {
        if (participant.IsMuted)
        {
            muteButton.image.sprite = muteSprite;
        }
        else
        {
            if (participant.SpeechDetected)
            {
                muteButton.image.sprite = speakingSprite;
            }
            else
            {
                muteButton.image.sprite = notSpeakingSprite;
            }
        }
    }

    private void OnMuteButtonClicked()
    {
        if (isMute)
        {
            participant.UnmutePlayerLocally();
            isMute = false;
        }
        else
        {
            participant.MutePlayerLocally();
            isMute = true;
        }
    }

    private void OnToggleValueChanged(bool isToggled)
    {
        onToggledParticipant?.Invoke(this, isToggled);
    }

    
    private void OnVivoxUserPrefabButtonClicked()
    {
        Debug.Log(participant.DisplayName);
    }

    private void SetDisplayName(string displayName)
    {
        displayNameText.text = $"{displayName}'s owner";
    }
}
