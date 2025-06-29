using UnityEngine;

public class HideDetailLabel : BasicInteractable
{
    [SerializeField] private LabelDetailController labelDetailController;
    public override void OnClick(PlayerController playerController)
    {
        base.OnClick(playerController);
        labelDetailController.HideDetailLabel();
        if (labelDetailController.IsShowingNewLabel)
        {
            EventHandler.Call_AfterReadLabel();
        }
    }
}
