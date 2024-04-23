using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyNote : MonoBehaviour
{
    public Action<StickyNote> onDeletedStickyNote;
    public Action<StickyNote> onAddStickyNote;
    
    [SerializeField] ClearButton clearButton;
    [SerializeField] DeleteButton deleteButton;
    [SerializeField] AddButton addButton;
    [SerializeField] TMPro.TMP_InputField inputField;

    private void Start()
    {
        clearButton.clearStickyNote += OnClearedStickyNote;
        deleteButton.deleteStickyNote += OnDeletedStickyNote;
        addButton.addStickyNote += OnAddStickyNote;
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
        inputField.text = "";
    }

    private void OnDestroy()
    {
        clearButton.clearStickyNote -= OnClearedStickyNote;
        deleteButton.deleteStickyNote -= OnDeletedStickyNote;
        addButton.addStickyNote -= OnAddStickyNote;
    }
}
