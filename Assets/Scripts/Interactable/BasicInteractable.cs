using UnityEngine;

public abstract class BasicInteractable : MonoBehaviour
{
    [SerializeField] private Collider2D hitbox;
    public virtual void OnClick(PlayerController playerController) { }
    public virtual void OnRelease() { }
    public virtual void OnExitHover() { }
    public virtual void OnHover() { }
    public virtual void Controlling(Vector3 pos, Vector3 delta) { }
    protected void DisableHitbox() => hitbox.enabled = false;
    protected void EnableHitbox() => hitbox.enabled = true;
}
