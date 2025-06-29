using UnityEngine;

public class DeadObject : MonoBehaviour
{
    [SerializeField] private string deadType;
    [SerializeField] private LastWords_TEXT textData;
    [SerializeField] private LastWords_AUDIO audioData;
    [SerializeField] private LastWords_IMAGE imgData;
    public bool HasText => textData != null;
    public bool HasAudio => audioData != null;
    public bool HasImg => imgData != null;
    public LastWords_TEXT GetTextData() => textData;
    public LastWords_AUDIO GetAudioData() => audioData;
    public LastWords_IMAGE GetImageData() => imgData;
    public string GetDeadsType() => deadType;
}
