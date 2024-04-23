using System;
using UnityEngine;

public class AddButton : MonoBehaviour
{
    public Action addStickyNote;

    private void OnEnable()
    {
        GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => OnAddStickyNotesButtonClicked());
    }
    private void OnDisable()
    {
        GetComponent<UnityEngine.UI.Button>().onClick.RemoveAllListeners();
    }

    private void OnAddStickyNotesButtonClicked()
    {
        addStickyNote?.Invoke();
    }
}
