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
    private float stateTimer = 3f;

    public override GameStateType m_gameState => GameStateType.Intro;

    public IntroState(float duration)
    {
        stateTimer = duration;
    }
    public override void EnterState(GameController context)
    {
        base.EnterState(context);
        context.SpeedUpStair(2f);
    }
    public override State<GameController> UpdateState(GameController context)
    {
        stateTimer -= Time.deltaTime;
        if (stateTimer <= 0)
            return new EntryState(context.GetNextEntryData());
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
        entryTrans.position = Vector3.LerpUnclamped(entryData.startPos, entryData.targetPos, EasingFunc.Easing.QuadEaseOut(tweenTimer / entryData.tweenDuration));
        entryTrans.localScale = Vector3.one * Mathf.LerpUnclamped(2f, 1f, EasingFunc.Easing.QuadEaseOut(tweenTimer / entryData.tweenDuration));
        if (tweenTimer >= entryData.tweenDuration)
        {
            context.StopStair(0.5f);
            return new GetMessageState(entryData.entryObject);
        }
        else
            return null;
    }
}

//获取物品遗言环节
public class GetMessageState : GameState
{
    private DeadObject deadObject;
    private bool insertFlag = false;

    public override GameStateType m_gameState => GameStateType.GetMessage;

    public GetMessageState(DeadObject targetObject)
    {
        insertFlag = false;
        deadObject = targetObject;
    }
    public override void EnterState(GameController context)
    {
        context.ReadDeadObject(deadObject);
        EventHandler.E_OnInsertLabel += InsertLabelHandler;
    }
    public override void ExitState(GameController context)
    {
        EventHandler.E_OnInsertLabel -= InsertLabelHandler;
    }
    public override State<GameController> UpdateState(GameController context)
    {
        if (insertFlag)
        {
            return new ChooseMark(deadObject);
        }
        return null;
    }
    void InsertLabelHandler(DragableNotes note) => insertFlag = true;
}

//选择物品墓志铭环节
public class ChooseMark : GameState
{
    public override GameStateType m_gameState => GameStateType.ChooseMark;
    private GameController context;
    private DeadObject deadObject;
    private bool hasRead = false;
    private bool trashFlag = false;
    public ChooseMark(DeadObject deadObject)
    {
        this.deadObject = deadObject;
    }
    public override void EnterState(GameController context)
    {
        base.EnterState(context);
        this.context = context;
        EventHandler.E_AfterReadLabel += AfterLabelHandler;
        EventHandler.E_OnTrashDeads += TrashDeadsHandler;
    }
    public override void ExitState(GameController context)
    {
        base.ExitState(context);
        EventHandler.E_AfterReadLabel -= AfterLabelHandler;
        EventHandler.E_OnTrashDeads -= TrashDeadsHandler;
    }
    public override State<GameController> UpdateState(GameController context)
    {
        if (trashFlag)
        {
            return new DeadEntry(deadObject);
        }
        return null;
    }
    void TrashDeadsHandler() => trashFlag = true;
    void AfterLabelHandler()
    {
        if (!hasRead)
        {
            hasRead = true;
            if (!context.IsEnding)
                context.StartGateOpenSeq();
            else
                context.StartEndingSeq();
        }
    }
}

//焚烧物品环节
public class DeadEntry : GameState
{
    private DeadObject deadObject;
    private Vector3 startPos;
    private Vector3 trashPoint;
    private Vector3 startScale;
    private float trashTimer = 0;
    private bool deadsTrashed = false;
    private bool reset = false;
    public override GameStateType m_gameState => GameStateType.Dead;

    public DeadEntry(DeadObject deadObject)
    {
        this.deadObject = deadObject;
        deadsTrashed = false;
        startPos = deadObject.transform.position;
        startScale = deadObject.transform.localScale;
    }
    public override void EnterState(GameController context)
    {
        trashTimer = 0;
        trashPoint = context.GetTrashPos();
        context.SpeedUpStair(0.5f);
    }
    public override State<GameController> UpdateState(GameController context)
    {
        trashTimer += Time.deltaTime;
        if (!deadsTrashed)
        {
            deadObject.transform.position = Vector3.Lerp(startPos, trashPoint, EasingFunc.Easing.QuadEaseIn(trashTimer / 1.5f));
            deadObject.transform.localScale = Vector3.Lerp(startScale, startScale * 0.4f, EasingFunc.Easing.QuadEaseIn(trashTimer / 1.5f));
        }
        if (trashTimer >= 1.5f && !deadsTrashed)
        {
            deadsTrashed = true;
            context.ClearDeads(deadObject);
        }
        if (trashTimer >= 2f && !reset)
        {
            reset = true;
            context.ResetFactory();
        }

        if (trashTimer >= 6f)
            {
                return new EntryState(context.GetNextEntryData());
            }
        return null;
    }
}

//游戏结局
public class Ending : GameState
{ 
    public override GameStateType m_gameState => GameStateType.Ending;
}
