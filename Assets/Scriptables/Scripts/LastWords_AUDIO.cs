using UnityEngine;

[CreateAssetMenu(fileName = "LastWords_AUDIO", menuName = "Scriptable Objects/LastWords/LastWords_AUDIO")]
public class LastWords_AUDIO : LastWords_SO
{
    public override WordsType wordsType => WordsType.Audio;
    [SerializeField, TextArea] private string audioText;
    public string GetAudioText() => audioText;
}
