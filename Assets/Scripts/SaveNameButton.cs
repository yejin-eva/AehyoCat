using System;
using TMPro;
using UnityEngine;

public class SaveNameButton : MonoBehaviour
{
    public Action onSaveNameButtonClicked;
    public TMPro.TextMeshProUGUI catNameText;
    private UnityEngine.UI.Button saveNameButton => GetComponent<UnityEngine.UI.Button>();

    private void Awake()
    {
        saveNameButton.onClick.AddListener(OnSaveNameButtonClicked);
    }

    private void OnDestroy()
    {
        saveNameButton.onClick.RemoveAllListeners();
    }

    private void OnSaveNameButtonClicked()
    {
        onSaveNameButtonClicked?.Invoke();
    }

    public string GetCatName()
    {
        return catNameText.text;
    }
}
