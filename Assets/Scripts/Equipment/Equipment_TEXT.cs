using TMPro;
using UnityEngine;

public class Equipment_TEXT : Equipment
{
    [SerializeField] private TextMeshPro text;
    [SerializeField] private float textMoveSpeed;
    [SerializeField] private float textMoveStep;
    [SerializeField] private float initTextPos = 4;
    private float textSize;
    private float moveTimer;
    public override void ProcessContent(LastWords_SO lastWords)
    {
        var words = lastWords as LastWords_TEXT;
        text.text = words.GetShowingText();
        text.transform.localPosition = Vector2.right * initTextPos;
        textSize = text.text.Length;
        moveTimer = 0;
        base.ProcessContent(lastWords);
    }
    void Update()
    {
        moveTimer += Time.deltaTime * textMoveSpeed;
        float moveDist = Mathf.FloorToInt(moveTimer / textMoveStep) * textMoveStep;
        text.transform.localPosition = Vector3.right * (-moveDist + initTextPos);
    }
}
