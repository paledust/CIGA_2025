using UnityEngine;

[CreateAssetMenu(fileName = "LastWords_AUDIO", menuName = "Scriptable Objects/LastWords/LastWords_AUDIO")]
public class LastWords_AUDIO : LastWords_SO
{
    public override WordsType wordsType => WordsType.Audio;
    [SerializeField] private float waveLength;
    [SerializeField] private float waveFreq;
    [SerializeField] private float waveSpeed;
    public WaveShapeData GetWaveShapeData() => new WaveShapeData(waveLength, waveFreq, waveSpeed);
}
