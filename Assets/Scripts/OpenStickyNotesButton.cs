using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenStickyNotesButton : MonoBehaviour
{
    [SerializeField] GameObject stickyNoteItem;
    UnityEngine.UI.Button openStickyNotesButton;

    private bool isOpen = false;


    private void Awake()
    {
        stickyNoteItem.SetActive(false);
        openStickyNotesButton = GetComponent<UnityEngine.UI.Button>();
    }
    
    private void OnEnable()
    {
        openStickyNotesButton.onClick.AddListener(() => OnOpenStickyNotesButtonClicked());
    }

    public void OnOpenStickyNotesButtonClicked()
    {
        isOpen = !isOpen;
        SetOpenStatus(isOpen);
    }

    public void SetOpenStatus(bool status)
    {
        isOpen = status;
        stickyNoteItem.SetActive(isOpen);
    }
}
