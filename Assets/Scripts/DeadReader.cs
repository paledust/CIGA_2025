using UnityEngine;

public enum WordsType
{
    Text,
    Audio,
    Image
}

public class DeadReader : MonoBehaviour
{
    [SerializeField] private Equipment_AUDIO equipment_AUDIO;
    [SerializeField] private Equipment_IMG equipment_IMG;
    [SerializeField] private Equipment_TEXT equipment_TEXT;
    public void ReadDeadObject(DeadObject deads)
    {
        if (deads.HasText)
        {
            equipment_TEXT.ProcessContent(deads.GetTextData());
        }
        if (deads.HasAudio)
        {
            equipment_AUDIO.ProcessContent(deads.GetAudioData());
        }
        if (deads.HasImg)
        {
            equipment_IMG.ProcessContent(deads.GetImageData());
        }
    }
    public void ClearRead()
    {
        equipment_TEXT.ClearContent();
        equipment_AUDIO.ClearContent();
        equipment_IMG.ClearContent();
    }
}
