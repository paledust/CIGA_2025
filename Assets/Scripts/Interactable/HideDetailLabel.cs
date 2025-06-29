using SimpleAudioSystem;
using UnityEngine;

public class HideDetailLabel : BasicInteractable
{
    [SerializeField] private string clickClip;
    [SerializeField] private LabelDetailController labelDetailController;
    public override void OnClick(PlayerController playerController)
    {
        base.OnClick(playerController);
        AudioManager.Instance.PlaySoundEffect(clickClip, 1);
        labelDetailController.HideDetailLabel();
        if (labelDetailController.IsShowingNewLabel)
        {
            EventHandler.Call_AfterReadLabel();
        }
    }
}
