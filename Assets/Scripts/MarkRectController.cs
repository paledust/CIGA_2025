using UnityEngine;

public class MarkRectController : MonoBehaviour
{
    public DragableNotes[] allNotes;
    public Rect DragRect;
    public Rect InsertRect;
    void Start()
    {
        foreach (var note in allNotes)
        {
            note.ChangeMoveRect(DragRect);
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(DragRect.center, DragRect.size);
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(InsertRect.center, InsertRect.size);
    }
}
