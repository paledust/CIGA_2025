using System.Collections;
using TMPro;
using UnityEngine;

public class Equipment_TEXT : Equipment
{
    [SerializeField] private TextMeshPro text;
    [SerializeField] private float textMoveSpeed;
    [SerializeField] private float textMoveStep;
    [SerializeField] private float initTextPos = 4;
    [SerializeField] private float recycleLength = 8;
    private float textSize;
    private float moveTimer;
    public override void ProcessContent(LastWords_SO lastWords)
    {
        var words = lastWords as LastWords_TEXT;
        text.text = words.GetShowingText();
        text.transform.localPosition = Vector2.right * initTextPos;
        StartCoroutine(coroutineGetBounds());
        moveTimer = 0;

        base.ProcessContent(lastWords);
    }
    IEnumerator coroutineGetBounds()
    {
        yield return null;
        textSize = text.textBounds.size.x;
    }
    void Update()
    {
        moveTimer += Time.deltaTime * textMoveSpeed;
        float moveDist = Mathf.FloorToInt(moveTimer / textMoveStep) * textMoveStep;
        text.transform.localPosition = Vector3.right * (-moveDist + initTextPos);
        if (moveDist > textSize+recycleLength)
        {
            moveTimer = 0;
        }
    }
}
