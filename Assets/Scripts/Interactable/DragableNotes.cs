using DG.Tweening;
using UnityEngine;

public class DragableNotes : BasicInteractable
{
    private Rect moveRect;
    private BoardBox insertingBox;
    private Vector2 targetPos;
    private Vector3 validPoint;
    private float initAngle;
    private bool locked;
    public override void OnClick(PlayerController playerController)
    {
        base.OnClick(playerController);
        targetPos = transform.position;
        validPoint = targetPos;
        playerController.HoldInteractable(this);
        initAngle = transform.rotation.eulerAngles.z;
    }
    public override void Controlling(Vector3 pos, Vector3 delta)
    {
        pos.x = Mathf.Clamp(pos.x, moveRect.min.x, moveRect.max.x);
        pos.y = Mathf.Clamp(pos.y, moveRect.min.y, moveRect.max.y);
        targetPos = Vector2.Lerp(targetPos, pos, Time.deltaTime * 7);

        transform.position = targetPos;
    }
    public override void OnRelease()
    {
        if (!insertingBox)
        {
            DisableHitbox();
            transform.DOMove(validPoint, 0.5f).SetEase(Ease.OutQuad).OnComplete(EnableHitbox);
        }
        else
        {
            locked = true;
            transform.DOMove(insertingBox.GetInsertPos(), 0.4f).SetEase(Ease.OutBack).OnComplete(()=>
            {
                EventHandler.Call_OnInsertLabel();
            });
            DisableHitbox();
        }
    }
    public void OnEnterInsertionZone(BoardBox boardBox)
    {
        insertingBox = boardBox;
        transform.DORotate(new Vector3(0, 0, 2.8f), 0.5f).SetEase(Ease.OutQuad);
    }
    public void OnExitInsertionZone(BoardBox boardBox)
    {
        insertingBox = null;
        if(!locked)
            transform.DORotate(new Vector3(0, 0, initAngle), 0.5f).SetEase(Ease.OutQuad);
    }
    public void ChangeMoveRect(Rect newRect) => moveRect = newRect;
}