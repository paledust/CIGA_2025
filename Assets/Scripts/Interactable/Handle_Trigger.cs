using UnityEngine;

public class Handle_Trigger : BasicInteractable
{
    [SerializeField] private Handle handle;
    [SerializeField] private float pushForce = 10;
    public override void OnClick(PlayerController playerController)
    {
        if (handle.m_isLocked)
        {
            DisableHitbox();
            handle.ShakeHandle(EnableHitbox);
        }
        else
        {
            playerController.HoldInteractable(this);
        }
    }
    public override void OnRelease()
    {
        DisableHitbox();
        handle.ReleaseHandle(EnableHitbox);
    }
    public override void Controlling(Vector3 pos, Vector3 delta)
    {
        handle.PushHandle(delta.y * pushForce, ()=>
        {
            EventHandler.Call_FlushInput();
        });
    }
}
