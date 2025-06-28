using UnityEngine;

[CreateAssetMenu(fileName = "LastWords_TEXT", menuName = "Scriptable Objects/LastWords/LastWords_TEXT")]
public class LastWords_TEXT : LastWords_SO
{
    public override WordsType wordsType => WordsType.Text;
    [SerializeField] private string showingText;
    public string GetShowingText() => showingText;
}
