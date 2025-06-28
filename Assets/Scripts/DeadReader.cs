using UnityEngine;

public enum WordsType
{
    Text,
    Audio,
    Image
}
public struct WaveShapeData
{
    public float waveLength;
    public float waveFreq;
    public float waveSpeed;
    public WaveShapeData(float length, float freq, float speed)
    {
        waveLength = length;
        waveFreq = freq;
        waveSpeed = speed;
    }
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
}
