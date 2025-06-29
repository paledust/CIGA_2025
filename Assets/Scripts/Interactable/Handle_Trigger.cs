using SimpleAudioSystem;
using UnityEngine;

public class Handle_Trigger : BasicInteractable
{
    [SerializeField] private Handle handle;
    [SerializeField] private float pushForce = 10;
    [SerializeField] private string clickClip = "sfx_joystick";
    public override void OnClick(PlayerController playerController)
    {
        AudioManager.Instance.PlaySoundEffect(clickClip, 1);
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
