using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct StickyNoteEntry
{
    public string stickyNoteContent;
    public Vector3 position;
}

[CreateAssetMenu(fileName = "StickyNotes", menuName = "StickyNotes")]
public class StickyNotesSO : ScriptableObject
{
    public List<StickyNoteEntry> stickyNotes = new List<StickyNoteEntry>();
}
