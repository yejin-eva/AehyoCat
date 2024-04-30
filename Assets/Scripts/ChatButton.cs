using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatButton : MonoBehaviour
{
    public Action onChatButton;
    private void Start()
    {
        GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => OnChatButtonClicked());
    }
    private void OnDestroy()
    {
        GetComponent<UnityEngine.UI.Button>().onClick.RemoveAllListeners();
    }
    private void OnChatButtonClicked()
    {
        onChatButton?.Invoke();
    }
}
