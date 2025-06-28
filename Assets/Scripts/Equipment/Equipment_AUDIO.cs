using DG.Tweening;
using UnityEngine;

public class Equipment_AUDIO : Equipment
{
    [SerializeField] private PerRenderWave perRenderWave;
    [SerializeField] private Equipment_AUDIO_CommandManager commandManager;
    readonly static string[] separators = new string[] { "==", "->" };
    public override void ProcessContent(LastWords_SO lastWords)
    {
        base.ProcessContent(lastWords);
        var contents = (lastWords as LastWords_AUDIO).GetAudioText().Split(separators[1]);

        Command<Equipment_AUDIO> headCommand = new C_Wait<Equipment_AUDIO>(0.5f);
        Command<Equipment_AUDIO> frontCommand = headCommand;
        for (int i = 0; i < contents.Length; i++)
        {
            string[] keyAndValue = contents[i].Split(separators[0], System.StringSplitOptions.None);
            string text = keyAndValue[0];
            float duration = -1f;
            if (keyAndValue.Length > 1) float.TryParse(keyAndValue[1], out duration);
            frontCommand = frontCommand.QueueCommand(new EQ_Show_Audio(text, duration))
                                       .QueueCommand(new C_Wait<Equipment_AUDIO>(0.1f));
        }
        commandManager.AddCommand(headCommand);
    }
    public override void ClearContent()
    {
        base.ClearContent();
        commandManager.AbortCommands();
    }
    public void TuneWaveSignal(float ampControl)
    {
        DOTween.Kill(perRenderWave);
        DOTween.To(() => perRenderWave.amplitudeControl, (x) => perRenderWave.amplitudeControl = x, ampControl, 0.5f);
    }
}