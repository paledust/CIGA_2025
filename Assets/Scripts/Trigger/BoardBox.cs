using UnityEngine;

public class BoardBox : MonoBehaviour
{
    [SerializeField] private MarkRectController markRectController;
    [SerializeField] private Transform insertPoint;
    
    void OnTriggerEnter2D(Collider2D other)
    {
        var note = other.GetComponent<DragableNotes>();
        if (note != null)
        {
            note.ChangeMoveRect(markRectController.InsertRect);
            note.OnEnterInsertionZone(this);
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        var note = other.GetComponent<DragableNotes>();
        if (note != null)
        {
            note.ChangeMoveRect(markRectController.DragRect);
            note.OnExitInsertionZone(this);
        }
    }
    public Vector3 GetInsertPos() => insertPoint.position;
}
