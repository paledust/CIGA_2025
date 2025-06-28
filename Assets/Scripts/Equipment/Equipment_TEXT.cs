using System.Collections;
using Febucci.UI;
using TMPro;
using UnityEngine;

public class Equipment_TEXT : Equipment
{
    [SerializeField] private TypewriterByCharacter typeWriter;
    private float textSize;
    private float moveTimer;
    void OnEnable()
    {
        EventHandler.E_OnInsertLabel += OnInsertLabelHandler;
    }
    void OnDisable()
    {
        EventHandler.E_OnInsertLabel -= OnInsertLabelHandler;
    }
    void OnInsertLabelHandler()
    {
        typeWriter.TextAnimator.SetText(string.Empty);
    }
    public override void ProcessContent(LastWords_SO lastWords)
    {
        var words = lastWords as LastWords_TEXT;
        typeWriter.ShowText(words.GetShowingText());
        moveTimer = 0;

        base.ProcessContent(lastWords);
    }
    public override void ClearContent()
    {
        typeWriter.TextAnimator.SetText(string.Empty);
    }
}
