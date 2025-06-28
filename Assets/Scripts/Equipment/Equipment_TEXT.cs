using System.Collections;
using Febucci.UI;
using TMPro;
using UnityEngine;

public class Equipment_TEXT : Equipment
{
    [SerializeField] private TypewriterByCharacter typeWriter;
    private float textSize;
    private float moveTimer;
    public override void ProcessContent(LastWords_SO lastWords)
    {
        var words = lastWords as LastWords_TEXT;
        typeWriter.ShowText(words.GetShowingText());
        moveTimer = 0;

        base.ProcessContent(lastWords);
    }
}
