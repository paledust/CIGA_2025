using DG.Tweening;
using UnityEngine;

public class DragableNotes : BasicInteractable
{
    [SerializeField] private SpriteRenderer noteRender;
    [SerializeField] private SpriteRenderer writingRender;
    private Rect moveRect;
    private Rect hangRect;
    private Vector2 targetPos;
    private Vector3 validPoint;
    private BoardBox insertingBox;
    private float initAngle;

    private string label = string.Empty;
    private string deadType = string.Empty;
    private int num = -1;

    private bool locked;
    private bool IsComplete = false;

    private bool IsLabeled => !string.IsNullOrEmpty(label);

    public void GiveLabelDetail(string label, string deadType, int num)
    {
        this.label = label;
        this.deadType = deadType;
        this.num = num;
        writingRender.gameObject.SetActive(true);
    }
    public override void OnClick(PlayerController playerController)
    {
        if (!IsLabeled)
        {
            targetPos = transform.position;
            validPoint = targetPos;
            playerController.HoldInteractable(this);
            initAngle = transform.rotation.eulerAngles.z;
        }
        else
        {
            EventHandler.Call_OnShowLabel(new LabelDetailData()
            {
                num = num,
                deadsType = deadType,
                label = label
            });
            if (!IsComplete)
            {
                IsComplete = true;
                transform.position = new Vector2(Random.Range(hangRect.min.x, hangRect.max.x), Random.Range(hangRect.min.y, hangRect.max.y));
                noteRender.color = Color.gray;
            }
        }
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
            transform.DOMove(insertingBox.GetInsertPos(), 0.4f).SetEase(Ease.OutBack).OnComplete(() =>
            {
                EventHandler.Call_OnInsertLabel(this);
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
        if (!locked)
            transform.DORotate(new Vector3(0, 0, initAngle), 0.5f).SetEase(Ease.OutQuad);
    }
    public void ChangeMoveRect(Rect newRect) => moveRect = newRect;
    public void GiveHangRect(Rect newRect) => hangRect = newRect;
}