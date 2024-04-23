using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyNotesButton : MonoBehaviour
{
    public Action stickyNoteButtonClicked;

    [SerializeField] StickyNotesContainer stickyNotesContainer;

    private UnityEngine.UI.Button stickyNotesButton;
    
    private void Awake()
    {
        stickyNotesButton = GetComponent<UnityEngine.UI.Button>();
    }
    
    private void OnEnable()
    {
        stickyNotesButton.onClick.AddListener(() => OnStickyNotesButtonClicked());
    }
    private void OnDisable()
    {
        stickyNotesButton.onClick.RemoveAllListeners();
    }
    public void OnStickyNotesButtonClicked()
    {
        stickyNotesContainer.SetOpenStatus(!stickyNotesContainer.IsOpen);
    }

    
}
