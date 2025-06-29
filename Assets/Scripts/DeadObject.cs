using DG.Tweening;
using UnityEngine;

public class DeadObject : MonoBehaviour
{
    [SerializeField] private LastWords_TEXT textData;
    [SerializeField] private LastWords_AUDIO audioData;
    [SerializeField] private LastWords_IMAGE imgData;
    [SerializeField] private SpriteRenderer talkSign;
    private string deadType;

    public bool HasText => textData != null;
    public bool HasAudio => audioData != null;
    public bool HasImg => imgData != null;
    void Start()
    {
        deadType = gameObject.name;
        Color col = talkSign.color;
        col.a = 0;
        talkSign.color = col;
    }
    public LastWords_TEXT GetTextData() => textData;
    public LastWords_AUDIO GetAudioData() => audioData;
    public LastWords_IMAGE GetImageData() => imgData;
    public string GetDeadsType() => deadType;
    public void BeginTalking()=>talkSign.DOFade(1, 0.1f);
    public void EndTalking()=>talkSign.DOFade(0, 0.1f);
}
