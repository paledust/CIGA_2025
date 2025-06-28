using TMPro;
using UnityEngine;

public class Equipment_TEXT : Equipment
{
    [SerializeField] private TextMeshPro text;
    [SerializeField] private float textMoveSpeed;
    [SerializeField] private float initTextPos;
    private float textSize;
    public override void ProcessContent(LastWords_SO lastWords)
    {
        var words = lastWords as LastWords_TEXT;
        text.text = words.GetShowingText();
        textSize = text.text.Length;
        base.ProcessContent(lastWords);
    }
    void Update()
    {

    }
}
