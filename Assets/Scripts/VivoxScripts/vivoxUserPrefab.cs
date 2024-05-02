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

    VivoxParticipant participant;
    private void Start()
    {
        if (participant.IsSelf)
        {
            toggleButton.gameObject.SetActive(false);
        }
        else
        {
            toggleButton.gameObject.SetActive(true);
        }
        this.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => OnVivoxUserLoginPrefabClicked());
        toggleButton.onValueChanged.AddListener((value) => OnToggleValueChanged(value));
    }

    private void OnToggleValueChanged(bool isToggled)
    {
        onToggledParticipant?.Invoke(this, isToggled);
    }

    public void Init(VivoxParticipant participant)
    {
        this.participant = participant;
        SetDisplayName(participant.DisplayName);
    }
    private void OnVivoxUserLoginPrefabClicked()
    {
        Debug.Log(participant.DisplayName);
    }

    private void SetDisplayName(string displayName)
    {
        displayNameText.text = $"{displayName}'s owner";
    }
}
