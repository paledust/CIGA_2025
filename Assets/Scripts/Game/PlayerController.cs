using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private PlayerInput playerInput;
    private BasicInteractable holdedInteractable;
    private BasicInteractable m_hoveringInteractable;
    private Vector2 mouseScrPos;
    private Vector2 mouseDelta;
    private Camera mainCam;
    void Awake()
    {
        mainCam = Camera.main;
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
                    }
                }
                else
                    ClearCurrentInteractable();
            }
            else
                ClearCurrentInteractable();
        }
        else
        {
            holdedInteractable.Controlling(mouseScrPos, mouseDelta);
        }
    }
    void ClearCurrentInteractable() {
        if (m_hoveringInteractable != null) {
            m_hoveringInteractable.OnExitHover();
            m_hoveringInteractable = null;
        }
    }
    public void HoldInteractable(BasicInteractable basicInteractable) => holdedInteractable = basicInteractable; 

    #region Input Event
    void OnTouch(InputValue value)
    {
        Debug.Log("Clicking");
        if (value.isPressed)
        {
            if (m_hoveringInteractable != null)
            {
                Debug.Log("OnClick");
                m_hoveringInteractable.OnClick(this);
            }
        }
        else
        {
            if (holdedInteractable != null)
            {
                holdedInteractable.OnRelease();
                holdedInteractable = null;
            }
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
}
