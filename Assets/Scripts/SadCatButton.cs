using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SadCatButton : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshProUGUI quotesText;

    private UnityEngine.UI.Button sadCatButton;

    private void Awake()
    {
        sadCatButton = GetComponent<UnityEngine.UI.Button>();
        this.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        sadCatButton.onClick.AddListener(() => OnSadCatButtonClicked());
    }
    private void OnDisable()
    {
        sadCatButton.onClick.RemoveAllListeners();
    }
    private void OnSadCatButtonClicked()
    {
        Debug.Log("Sad cat button clicked");
    }

    private void SetHungerMessageForSeconds(float seconds = 2f)
    {

    }
}
