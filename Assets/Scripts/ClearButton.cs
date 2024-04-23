using System;
using UnityEngine;

public class ClearButton : MonoBehaviour
{
    public Action clearStickyNote;

    private void OnEnable()
    {
        GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => OnClearButtonClicked());
    }
    private void OnDisable()
    {
        GetComponent<UnityEngine.UI.Button>().onClick.RemoveAllListeners();
    }

    private void OnClearButtonClicked()
    {
        clearStickyNote?.Invoke();
    }
}
