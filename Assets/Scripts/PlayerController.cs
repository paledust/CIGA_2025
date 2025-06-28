using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private CursorState_SO cursorState_SO;
    private PlayerInput playerInput;
    private BasicInteractable holdedInteractable;
    private BasicInteractable m_hoveringInteractable;
    private Vector2 mouseScrPos;
    private Vector2 mouseDelta;
    private Camera mainCam;
    private CURSOR_STATE currentCursorState = CURSOR_STATE.DEFAULT;
    void Awake()
    {
        mainCam = Camera.main;
        UpdateCursorState(currentCursorState);
    }
    void Update()
    {
        if (holdedInteractable == null)
        {
            Ray ray = mainCam.ScreenPointToRay(mouseScrPos);
            var hit = Physics2D.Raycast(ray.origin, ray.direction, 100, 1 << Service.InteractableLayer);
            if (hit.collider != null)
            {
                var hit_Interactable = hit.collider.GetComponent<BasicInteractable>();
                if (hit_Interactable != null)
                {
                    if (m_hoveringInteractable != hit_Interactable)
                    {
                        if (m_hoveringInteractable != null) m_hoveringInteractable.OnExitHover();
                        m_hoveringInteractable = hit_Interactable;
                        m_hoveringInteractable.OnHover();
                        if(!holdedInteractable) UpdateCursorState(CURSOR_STATE.HOVER);
                    }
                }
                else
                    ClearHoveringInteractable();
            }
            else
                ClearHoveringInteractable();
        }
        else
        {
            holdedInteractable.Controlling(mouseScrPos, mouseDelta);
        }
    }
    void ClearHoveringInteractable()
    {
        if (m_hoveringInteractable != null)
        {
            m_hoveringInteractable.OnExitHover();
            m_hoveringInteractable = null;
        }
        if (!holdedInteractable) UpdateCursorState(CURSOR_STATE.DEFAULT);
    }
    public void HoldInteractable(BasicInteractable basicInteractable) => holdedInteractable = basicInteractable;
    public void ReleaseHoldInteractable()
    {
        if(holdedInteractable != null){
            var holding = holdedInteractable;
            holdedInteractable = null;
            holding.OnRelease();
        }
        if(!m_hoveringInteractable) UpdateCursorState(CURSOR_STATE.DEFAULT);
        else UpdateCursorState(CURSOR_STATE.HOVER);
    }

    #region Input Event
    void OnTouch(InputValue value)
    {
        if (value.isPressed)
        {
            if (m_hoveringInteractable != null)
            {
                m_hoveringInteractable.OnClick(this);
            }
        }
        else
        {
            ReleaseHoldInteractable();
        }
    }
    void OnMove(InputValue value)
    {
        mouseScrPos = value.Get<Vector2>();
    }
    void OnDelta(InputValue value)
    {
        mouseDelta = value.Get<Vector2>();
    }
    #endregion

    public void UpdateCursorState(CURSOR_STATE newState){
        if(currentCursorState != newState){
            currentCursorState = newState;
            var data = cursorState_SO.GetCursorStateData(currentCursorState);
            Cursor.SetCursor(data.texture, data.offset, CursorMode.Auto);
        }
    }
}
