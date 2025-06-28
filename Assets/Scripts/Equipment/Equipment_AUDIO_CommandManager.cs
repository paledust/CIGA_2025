using UnityEngine;

public class Equipment_AUDIO_CommandManager : CommandManager<Equipment_AUDIO> { }
public class EQ_AUDIO_Command : Command<Equipment_AUDIO> { }
public class EQ_Show_Audio : EQ_AUDIO_Command
{
    private float duration;
    private string content;
    private bool isExcuted;
    private float timer;
    public EQ_Show_Audio(string content, float duration)
    {
        timer = 0;
        isExcuted = false;
        this.duration = duration;
    }
    protected override void Init()
    {
        base.Init();
    }
    internal override void CommandUpdate(Equipment_AUDIO context)
    {
        base.CommandUpdate(context);
        if (!isExcuted)
        {
            isExcuted = true;
            context.OnGetSignal();
        }
        timer += Time.deltaTime;
        if (timer > duration)
        {
            context.OnLostSignal();
            SetStatus(CommandStatus.Success);
            return;
        }
    }

}