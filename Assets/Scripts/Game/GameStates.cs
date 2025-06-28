using UnityEngine;

[System.Serializable]
public enum GameStateType
{
    None, //空状态
    Intro, //开头
    Entry, //物体进入
    GetMessage, //获取遗言
    ChooseMark, //选择标签
    Dead, //报废物品
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
            return new EntryState(context.GetEntryData());
        else
            return null;
    }
}

//废弃物体进入环节
public class EntryState : GameState
{
    public struct EntryData
    {
        public DeadObject entryObject;
        public Vector3 targetPos;
        public Vector3 startPos;
        public float tweenDuration;
    }
    private Transform entryTrans;
    private EntryData entryData;
    private float tweenTimer;
    public override GameStateType m_gameState => GameStateType.Entry;
    public EntryState(EntryData _entryData)
    {
        entryData = _entryData;
        entryTrans = entryData.entryObject.transform;
        tweenTimer = 0;
    }
    public override void EnterState(GameController context)
    {
        base.EnterState(context);
        entryData.entryObject.gameObject.SetActive(true);
        entryData.entryObject.transform.position = entryData.startPos;
    }
    public override State<GameController> UpdateState(GameController context)
    {
        tweenTimer += Time.deltaTime;
        entryTrans.position = Vector3.LerpUnclamped(entryData.startPos, entryData.targetPos, EasingFunc.Easing.BackEaseOut(tweenTimer / entryData.tweenDuration));
        if (tweenTimer >= entryData.tweenDuration)
            return new GetMessageState(entryData.entryObject);
        else
            return null;
    }
}

//获取物品遗言环节
public class GetMessageState : GameState
{
    private DeadObject deadObject;
    public override GameStateType m_gameState => GameStateType.GetMessage;
    public GetMessageState(DeadObject targetObject)
    {
        deadObject = targetObject;
    }
    public override void EnterState(GameController context)
    {
        context.ReadDeadObject(deadObject);
    }
    public override State<GameController> UpdateState(GameController context)
    {
        return null;
    }
    
}

//选择物品墓志铭环节
public class ChooseMark : GameState
{ 
    public override GameStateType m_gameState => GameStateType.ChooseMark;
}

//焚烧物品环节
public class DeadEntry : GameState
{ 
    public override GameStateType m_gameState => GameStateType.Dead;
}

//游戏结局
public class Ending : GameState
{ 
    public override GameStateType m_gameState => GameStateType.Ending;
}
