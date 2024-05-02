using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vivoxUserLoginPrefab : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI displayNameText;

    private string userId;
    private void Start()
    {
        this.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => OnVivoxUserLoginPrefabClicked());
    }

    private void OnVivoxUserLoginPrefabClicked()
    {
        Debug.Log(userId);
    }
    public void SetUserId(string userId)
    {
        this.userId = userId;
    }

    public void SetDisplayName(string displayName)
    {
        displayNameText.text = $"{displayName}'s owner";
    }
}
