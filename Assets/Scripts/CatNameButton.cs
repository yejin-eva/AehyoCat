using System;
using UnityEngine;

public class CatNameButton : MonoBehaviour
{
    public Action onCatNameButtonClicked;
    public TMPro.TextMeshProUGUI CatNameText => catNameText;
    [SerializeField] private TMPro.TextMeshProUGUI catNameText;

    private UnityEngine.UI.Button catNameButton => GetComponent<UnityEngine.UI.Button>();
    private void Awake()
    {
        SetCatName("Aehyo");
    }

    private void OnEnable()
    {
        catNameButton.onClick.AddListener(() => OnCatNameButtonClicked());
    }

    private void OnCatNameButtonClicked()
    {
        onCatNameButtonClicked?.Invoke();
    }

    private void OnDisable()
    {
        catNameButton.onClick.RemoveAllListeners();
    }

    public void SetCatName(string name)
    {
        catNameText.text = name;
    }
}
