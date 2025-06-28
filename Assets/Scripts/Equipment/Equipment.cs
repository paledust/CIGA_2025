using UnityEngine;

public abstract class Equipment : MonoBehaviour
{
    [SerializeField] private SpriteRenderer ledRender;
    void Awake() => this.enabled = false;
    public virtual void ProcessContent(LastWords_SO lastWords) { this.enabled = true; }
    public virtual void ClearContent(){}
}
