using UnityEngine;

public abstract class Equipment : MonoBehaviour
{
    [SerializeField] private SpriteRenderer ledRender;
    public virtual void ProcessContent(LastWords_SO lastWords) {}
    public virtual void ClearContent(){}
}
