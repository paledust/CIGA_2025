using System.Collections;
using SimpleAudioSystem;
using UnityEngine;
using UnityEngine.Playables;

public class GameController : MonoBehaviour
{
    [SerializeField, ShowOnly] private GameStateType currentStateType = GameStateType.None;
    [Header("dispose obejct")]
    [SerializeField] private int startDisposeObjIndex = 0;
    [SerializeField] private DeadObject[] deadsInLine;
    [Header("ref Point")]
    [SerializeField] private Transform preparePoint;
    [SerializeField] private Transform viewPoint;
    [SerializeField] private Transform trashPoint;
    [Header("Timing")]
    [SerializeField] private float introTime = 3;
    [SerializeField] private float entryTime = 2f;
    [Header("Read Deads")]
    [SerializeField] private DeadReader deadReader;
    [Header("Init Game")]
    [SerializeField] private string roomToneClip;
    [Header("Handle")]
    [SerializeField] private Handle handle;
    [Header("Time line")]
    [SerializeField] private PlayableDirector TL_GateOpen;
    [SerializeField] private PlayableDirector TL_GateClose;

    private GameState currentState;
    private int currentDeadsIndex = 0;

    #region Unity Life Cycle

    void Start()
    {
        currentState = new IntroState(introTime);
        currentDeadsIndex = startDisposeObjIndex;
        AudioManager.Instance.PlayAmbience(roomToneClip, true, 1, true);
    }
    void Update()
    {
        currentStateType = currentState.m_gameState;
        State<GameController> newState = currentState.UpdateState(this);
        if (newState != null)
        {
            currentState.ExitState(this);
            currentState = newState as GameState;
            currentState.EnterState(this);
        }
    }
    #endregion

    #region Game Function
    public void PrepareDeadObj()
    {
        deadsInLine[currentDeadsIndex].gameObject.SetActive(true);
        deadsInLine[currentDeadsIndex].transform.position = preparePoint.position;
    }
    public EntryState.EntryData GetNextEntryData()
    {
        var entryData = new EntryState.EntryData()
        {
            entryObject = deadsInLine[currentDeadsIndex],
            startPos = preparePoint.position,
            targetPos = viewPoint.position,
            tweenDuration = entryTime
        };
        currentDeadsIndex++;
        return entryData;
    }
    public void ReadDeadObject(DeadObject deads) => deadReader.ReadDeadObject(deads);
    public void ResetFactory()
    {
        TL_GateClose.Play();
        handle.ResetHandle();
    }
    public void ClearDeads(DeadObject deadObject)
    {
        Destroy(deadObject.gameObject);
        deadReader.ClearRead();
    }
    public Vector3 GetTrashPos() => trashPoint.position;
    public void StartGateOpenSeq()
    {
        StartCoroutine(coroutineGateOpen());
    }
    #endregion
    IEnumerator coroutineGateOpen()
    {
        TL_GateOpen.Play();
        yield return new WaitForSeconds((float)TL_GateOpen.duration);
        handle.UnlockHandle();
    }
}