using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyNote : MonoBehaviour
{
    public bool IsOpen => isOpen;

    [SerializeField] ExitStickyNotesButton closeStickyNotesButton;
    [SerializeField] ClearButton clearButton;
    [SerializeField] TMPro.TMP_InputField inputField;

    private bool isOpen = false;

    private void Awake()
    {
        SetOpenStatus(isOpen);
    }

    private void Start()
    {
        closeStickyNotesButton.closeStickyNote += OnClosedStickyNote;
        clearButton.clearStickyNote += OnClearedStickyNote;
    }

    private void OnClosedStickyNote()
    {
        SetOpenStatus(false);
    }
    private void OnClearedStickyNote()
    {
        inputField.text = "";
    }
    public void SetOpenStatus(bool status)
    {
        isOpen = status;
        this.gameObject.SetActive(isOpen);
    }
}
