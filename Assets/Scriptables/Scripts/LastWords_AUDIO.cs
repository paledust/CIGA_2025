using UnityEngine;

[CreateAssetMenu(fileName = "LastWords_AUDIO", menuName = "Scriptable Objects/LastWords/LastWords_AUDIO")]
public class LastWords_AUDIO : LastWords_SO
{
    public override WordsType wordsType => WordsType.Audio;
    [SerializeField] private float waveLength = 1;
    [SerializeField] private float waveFreq = 1;
    [SerializeField] private float waveSpeed = 1;
    public WaveShapeData GetWaveShapeData() => new WaveShapeData(waveLength, waveFreq, waveSpeed);
}
