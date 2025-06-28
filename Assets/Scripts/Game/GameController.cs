using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField, ShowOnly] private GameStateType currentStateType = GameStateType.None;
    [Header("dispose obejct")]
    [SerializeField] private int startDisposeObjIndex = 0;
    [SerializeField] private DeadObject[] deadsInLine;
    [Header("ref Point")]
    [SerializeField] private Transform preparePoint;
    [SerializeField] private Transform viewPoint;
    [Header("Timing")]
    [SerializeField] private float introTime = 3;
    [SerializeField] private float entryTime = 2f;
    [Header("Read Deads")]
    [SerializeField] private DeadReader deadReader;

    private GameState currentState;
    private int currentDeadsIndex = 0;

    #region Unity Life Cycle
    void Start()
    {
        currentState = new IntroState(introTime);
        currentDeadsIndex = startDisposeObjIndex;
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
    public void PrepareDeadObj()
    {
        deadsInLine[currentDeadsIndex].gameObject.SetActive(true);
        deadsInLine[currentDeadsIndex].transform.position = preparePoint.position;
    }
    public EntryState.EntryData GetEntryData()
    {
        return new EntryState.EntryData()
        {
            entryObject = deadsInLine[currentDeadsIndex],
            startPos = preparePoint.position,
            targetPos = viewPoint.position,
            tweenDuration = entryTime
        };
    }
    public void ReadDeadObject(DeadObject deads)
    {
        deadReader.ReadDeadObject(deads);
    }
    #endregion
}