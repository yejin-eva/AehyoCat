using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenStickyNotesButton : MonoBehaviour
{
    public Action stickyNoteButtonClicked;

    [SerializeField] StickyNote stickyNote;

    private UnityEngine.UI.Button openStickyNotesButton;
    
    private void Awake()
    {
        openStickyNotesButton = GetComponent<UnityEngine.UI.Button>();
    }
    
    private void OnEnable()
    {
        openStickyNotesButton.onClick.AddListener(() => OnStickyNotesButtonClicked());
    }
    private void OnDisable()
    {
        openStickyNotesButton.onClick.RemoveAllListeners();
    }
    public void OnStickyNotesButtonClicked()
    {
        stickyNote.SetOpenStatus(!stickyNote.IsOpen);
    }

    
}
