using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseStickyNotesButton : MonoBehaviour
{
    public Action closeStickyNote;

    private void OnEnable()
    {
        GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => OnCloseStickyNotesButtonClicked());
    }
    private void OnDisable()
    {
        GetComponent<UnityEngine.UI.Button>().onClick.RemoveAllListeners();
    }

    private void OnCloseStickyNotesButtonClicked()
    {
        closeStickyNote?.Invoke();
    }

    
}
