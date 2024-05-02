using System.Collections;
using System.Collections.Generic;
using Unity.Services.Vivox;
using UnityEngine;

public class vivoxUserPrefab : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI displayNameText;

    private string userId;
    private void Start()
    {
        this.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => OnVivoxUserLoginPrefabClicked());
    }
    public void Init(VivoxParticipant participant)
    {
        SetDisplayName(participant.DisplayName);
    }
    private void OnVivoxUserLoginPrefabClicked()
    {
        Debug.Log(userId);
    }

    private void SetDisplayName(string displayName)
    {
        displayNameText.text = $"{displayName}'s owner";
    }
}
