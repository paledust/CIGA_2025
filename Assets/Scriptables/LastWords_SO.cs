using UnityEngine;

public abstract class LastWords_SO : ScriptableObject
{

    public virtual WordsType wordsType { get; }
    [SerializeField] private string[] mark;
    public string[] GetMarks() => mark;
}