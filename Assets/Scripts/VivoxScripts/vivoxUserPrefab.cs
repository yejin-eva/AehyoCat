using System;
using System.Collections;
using System.Collections.Generic;
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

    VivoxParticipant participant;

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
        SetDisplayName(participant.DisplayName);
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
