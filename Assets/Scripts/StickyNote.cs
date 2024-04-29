using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StickyNote : MonoBehaviour
{
    public Action<StickyNote> onDeletedStickyNote;
    public Action<StickyNote> onAddStickyNote;
    public string NoteContent => noteContent;
    
    [SerializeField] ClearButton clearButton;
    [SerializeField] DeleteButton deleteButton;
    [SerializeField] AddButton addButton;
    [SerializeField] TMPro.TMP_InputField inputField;

    private string noteContent = "";

    private void OnEnable()
    {
        inputField.text = noteContent;
    }
    private void Start()
    {
        clearButton.clearStickyNote += OnClearedStickyNote;
        deleteButton.deleteStickyNote += OnDeletedStickyNote;
        addButton.addStickyNote += OnAddStickyNote;
    }

    public void SetContent(string content)
    {
        noteContent = content;
        inputField.text = noteContent;
    }

    private void OnAddStickyNote()
    {
        onAddStickyNote?.Invoke(this);
    }

    private void OnDeletedStickyNote()
    {
        onDeletedStickyNote?.Invoke(this);
    }

    private void OnClearedStickyNote()
    {
        SetContent("");
    }

    private void OnDisable()
    {
        noteContent = inputField.text;
    }

    private void OnDestroy()
    {
        clearButton.clearStickyNote -= OnClearedStickyNote;
        deleteButton.deleteStickyNote -= OnDeletedStickyNote;
        addButton.addStickyNote -= OnAddStickyNote;
    }
}
