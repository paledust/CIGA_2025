using UnityEngine;

public class DeadObject : MonoBehaviour
{
    [SerializeField] private LastWords_TEXT textData;
    [SerializeField] private LastWords_AUDIO audioData;
    [SerializeField] private LastWords_IMAGE imgData;
    private string deadType;

    public bool HasText => textData != null;
    public bool HasAudio => audioData != null;
    public bool HasImg => imgData != null;
    void Start()
    {
        deadType = gameObject.name;
    }
    public LastWords_TEXT GetTextData() => textData;
    public LastWords_AUDIO GetAudioData() => audioData;
    public LastWords_IMAGE GetImageData() => imgData;
    public string GetDeadsType() => deadType;
}
