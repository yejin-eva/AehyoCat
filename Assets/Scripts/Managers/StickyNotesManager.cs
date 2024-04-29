using UnityEngine;

public class StickyNotesManager : MonoBehaviour
{
    public bool IsEnabledStickyNotesOpen => isEnabledStickyNotesOpen;

    [SerializeField] GameObject stickyNote;
    [SerializeField] Transform enabledStickyNotes;
    [SerializeField] private StickyNotesSO stickyNotesSO;

    private bool isEnabledStickyNotesOpen = false;
    private void Awake()
    {
        SetOpenStatus(isEnabledStickyNotesOpen);
    }
    private void OnEnable()
    {
        LoadSavedStickyNote();
        if (enabledStickyNotes.childCount == 0)
        {
            CreateStickyNote();
        }
    }
    private void LoadSavedStickyNote()
    {
        foreach(var savedNote in stickyNotesSO.stickyNotes)
        {
            GameObject newStickyNote = CreateStickyNote();
            StickyNote stickyNoteComponent = newStickyNote.GetComponent<StickyNote>();
            
            stickyNoteComponent.SetContent(savedNote.stickyNoteContent);
            newStickyNote.transform.position = savedNote.position;

            newStickyNote.SetActive(true);
        }
    }
    private void SaveStickyNote()
    {
        stickyNotesSO.stickyNotes.Clear();

        for(int i = 0; i < enabledStickyNotes.childCount; i++)
        {
            StickyNote stickyNote = enabledStickyNotes.GetChild(i).GetComponent<StickyNote>();
            if (stickyNote != null)
            {
                StickyNoteEntry entry = new StickyNoteEntry()
                {
                    stickyNoteContent = stickyNote.NoteContent,
                    position = stickyNote.transform.position,
                };
                stickyNotesSO.stickyNotes.Add(entry);
            }

        }
    }
    private GameObject CreateStickyNote()
    {
        GameObject newStickyNote = Instantiate(stickyNote, enabledStickyNotes);
        StickyNote stickyNoteComponent = newStickyNote.GetComponent<StickyNote>();
        
        stickyNoteComponent.onDeletedStickyNote += OnDeletedStickyNote;
        stickyNoteComponent.onAddStickyNote += OnAddStickyNote;

        newStickyNote.SetActive(true);

        return newStickyNote;

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
        if (enabledStickyNotes.childCount == 1)
        {
            SetOpenStatus(false);
        }
    }
    public void SetOpenStatus(bool isOpen)
    {
        this.isEnabledStickyNotesOpen = isOpen;
        enabledStickyNotes.gameObject.SetActive(isOpen);

    }

    private void OnDestroy()
    {
        SaveStickyNote();
    }

}
