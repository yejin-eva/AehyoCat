using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StickyNotes", menuName = "StickyNotes")]
public class StickyNotesSO : ScriptableObject
{
    public List<StickyNote> stickyNotes = new List<StickyNote>();
    
}
