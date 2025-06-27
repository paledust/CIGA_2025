using UnityEngine;

[System.Serializable]
public enum GameStateType
{
    None, //空状态
    Intro, //开头
    Entry, //物体进入
    GetMessage, //获取遗言
    ChooseMark, //选择标签
    Dispose, //报废物品
    Ending //结局
}
public abstract class GameState : State<GameController>
{
    public virtual GameStateType m_gameState{ get; }
}

//游戏入场环节
public class IntroState : GameState
{
    public override GameStateType m_gameState => GameStateType.Intro;
    private float stateTimer = 3f;
    public IntroState(float duration)
    {
        stateTimer = duration;
    }
    public override State<GameController> UpdateState(GameController context)
    {
        stateTimer -= Time.deltaTime;
        if (stateTimer <= 0)
        {
            return null;
        }
        return new EntryState(context.GetEntryData());
    }
}

//废弃物体进入环节
public class EntryState : GameState
{
    public struct EntryData
    {
        public Transform entryTrans;
        public Vector3 targetPos;
        public Vector3 startPos;
        public float tweenDuration;
    }
    private EntryData entryData;
    private float tweenTimer;
    public override GameStateType m_gameState => GameStateType.Entry;
    public EntryState(EntryData _entryData)
    {
        entryData = _entryData;
        tweenTimer = 0;
    }
    public override void EnterState(GameController context)
    {
        base.EnterState(context);
        entryData.entryTrans.position = entryData.startPos;
    }
    public override State<GameController> UpdateState(GameController context)
    {
        tweenTimer += Time.deltaTime;
        entryData.entryTrans.position = Vector3.Lerp(entryData.startPos, entryData.targetPos, EasingFunc.Easing.BackEaseOut(tweenTimer / entryData.tweenDuration));
        return null;
    }
}

//获取物品遗言环节
public class GetMessageState : GameState
{
    public override GameStateType m_gameState => GameStateType.GetMessage;
    
}

//选择物品墓志铭环节
public class ChooseMark : GameState
{ 
    public override GameStateType m_gameState => GameStateType.ChooseMark;
}

//焚烧物品环节
public class DisposeEntry : GameState
{ 
    public override GameStateType m_gameState => GameStateType.Dispose;
}

//游戏结局
public class Ending : GameState
{ 
    public override GameStateType m_gameState => GameStateType.Ending;
}
