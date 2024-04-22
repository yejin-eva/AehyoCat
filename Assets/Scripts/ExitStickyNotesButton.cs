using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitStickyNotesButton : MonoBehaviour
{
    [SerializeField] OpenStickyNotesButton openStickyNotesButton;

    private void OnEnable()
    {
        GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => OnExitStickyNotesButtonClicked());
    }
    private void OnDisable()
    {
        GetComponent<UnityEngine.UI.Button>().onClick.RemoveAllListeners();
    }

    private void OnExitStickyNotesButtonClicked()
    {
        openStickyNotesButton.SetOpenStatus(false);
    }
}
