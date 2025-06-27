using UnityEngine;
using DG.Tweening;
using System;
public class GameController : MonoBehaviour
{
    [SerializeField, ShowOnly] private GameStateType currentStateType = GameStateType.None;
    [SerializeField] private int startDisposeObjIndex = 0;
    [SerializeField] private Transform preparePoint;
    [SerializeField] private Transform viewPoint;
    [SerializeField] private DisposeObject[] disposeObjectsInLine;

    private GameState currentState;
    private int currentDisposeIndex = 0;

    #region Unity Life Cycle
    void Start()
    {
        currentState = new IntroState(3f);
        currentDisposeIndex = startDisposeObjIndex;
    }
    void OnEnable()
    {

    }
    void OnDisable()
    {

    }
    void Update()
    {
        currentStateType = currentState.m_gameState;
        State<GameController> newState = currentState.UpdateState(this);
        if (newState != null)
        {
            currentState = newState as GameState;
            currentState.EnterState(this);
        }
    }
    #endregion

    #region Game Function
    public void PrepareDisposeObj()
    {
        disposeObjectsInLine[currentDisposeIndex].gameObject.SetActive(true);
        disposeObjectsInLine[currentDisposeIndex].transform.position = preparePoint.position;
    }
    public EntryState.EntryData GetEntryData()
    {
        return new EntryState.EntryData()
        {
            entryTrans = disposeObjectsInLine[currentDisposeIndex].transform,
            startPos = preparePoint.position,
            targetPos = viewPoint.position,
            tweenDuration = 4.0f
        };
    }
#endregion
}