using UnityEngine;

public class StickyNotesManager : MonoBehaviour
{
    public bool IsOpen => isOpen;

    [SerializeField] GameObject stickyNote;
    [SerializeField] Transform enabledStickyNotes;

    private bool isOpen = false;
    private void Awake()
    {
        SetOpenStatus(isOpen);
    }
    private void OnEnable()
    {
        if (enabledStickyNotes.childCount == 0)
        {
            CreateStickyNote();
        }
    }

    private void CreateStickyNote()
    {
        GameObject newStickyNote = Instantiate(stickyNote, enabledStickyNotes);
        StickyNote stickyNoteComponent = newStickyNote.GetComponent<StickyNote>();
        
        stickyNoteComponent.onDeletedStickyNote += OnDeletedStickyNote;
        stickyNoteComponent.onAddStickyNote += OnAddStickyNote;

        newStickyNote.SetActive(true);

    }

    private void OnAddStickyNote(StickyNote note)
    {
        CreateStickyNote();
    }

    private void OnDeletedStickyNote(StickyNote note)
    {
        note.onDeletedStickyNote -= OnDeletedStickyNote;
        note.onAddStickyNote -= OnAddStickyNote;

        Destroy(note.gameObject);
        Debug.Log(enabledStickyNotes.childCount);
        if (enabledStickyNotes.childCount == 1)
        {
            SetOpenStatus(false);
        }
    }
    public void SetOpenStatus(bool isOpen)
    {
        this.isOpen = isOpen;
        gameObject.SetActive(isOpen);
    }

}