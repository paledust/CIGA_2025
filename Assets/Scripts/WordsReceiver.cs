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

public class WordsReceiver : MonoBehaviour
{
    public void ReadDisposeObject(DeadObject disposeObject)
    {

    }
}
