using System;
using System.Collections;
using UnityEngine;

public class Handle : MonoBehaviour
{
    [SerializeField] private bool isLocked = true;
    [SerializeField, Range(0, 1)] private float ratio;
    [SerializeField] private Transform handle_root;
    [SerializeField] private float handleOffset;
    [SerializeField] private float handleScale;
    [SerializeField] private Transform head_root;
    [SerializeField] private float headOffset;
    [SerializeField] private float headScale;
    [Header("Shake")]
    [SerializeField] private AnimationCurve shakeCurve;
    [Header("Push Handle")]
    [SerializeField] private float threashold = 0.4f;
    [SerializeField] private Handle_Trigger handle_Trigger;
    private Vector3 initHandlePos;
    private Vector3 initHeadPos;
    private Vector3 initHandleScale;
    private bool isPushing;
    public bool m_isLocked => isLocked;
    void Start()
    {
        initHandlePos = handle_root.localPosition;
        initHandleScale = handle_root.localScale;
        initHeadPos = head_root.localPosition;
    }
    // Update is called once per frame
    void Update()
    {
        handle_root.localPosition = Mathf.LerpUnclamped(0, handleOffset, ratio) * Vector3.up + initHandlePos;
        handle_root.localScale = Mathf.LerpUnclamped(0, handleScale, ratio) * Vector3.up + initHandleScale;
        head_root.localPosition = Mathf.LerpUnclamped(0, headOffset, ratio) * Vector3.up + initHeadPos;
        head_root.localScale = Vector3.one * Mathf.LerpUnclamped(1, headScale, ratio);
    }

    #region Operate Handle
    public void ShakeHandle(Action OnComplete)
    {
        StartCoroutine(coroutineShakeHandle(OnComplete));
    }
    public void PushHandle(float force, Action Success)
    {
        ratio += force * Time.deltaTime;
        ratio = Mathf.Min(1, ratio);
        if (ratio > threashold)
        {
            isPushing = true;
            Success?.Invoke();
            StartCoroutine(coroutinePushToEnd());
        }
    }
    public void ReleaseHandle(Action OnComplete)
    {
        if(!isPushing)
        {
            StartCoroutine(coroutineReturnHandle(OnComplete));
        }
    }
    public void ResetHandle()
    {
        StartCoroutine(coroutineResetHandle());
    }
    public void UnlockHandle() => isLocked = false;
    #endregion

    IEnumerator coroutineResetHandle()
    {
        isLocked = true;
        yield return new WaitForLoop(1f, (t) =>
        {
            ratio = Mathf.Lerp(1, 0, EasingFunc.Easing.BounceEaseOut(t));
        });
        handle_Trigger.EnableHitbox();
    }
    IEnumerator coroutineReturnHandle(Action OnComplete)
    {
        float initRatio = ratio;
        yield return new WaitForLoop(0.25f, (t) =>
        {
            ratio = Mathf.Lerp(initRatio, 0, EasingFunc.Easing.BounceEaseOut(t));
        });
        OnComplete?.Invoke();
    }
    IEnumerator coroutinePushToEnd()
    {
        float initRatio = ratio;
        yield return new WaitForLoop(0.2f, (t) =>
        {
            ratio = Mathf.LerpUnclamped(initRatio, 1, EasingFunc.Easing.QuadEaseOut(t));
        });
        isPushing = false;
        EventHandler.Call_OnBeginTrash();
    }
    IEnumerator coroutineShakeHandle(Action OnComplete)
    {
        yield return new WaitForLoop(0.5f, (t) =>
        {
            ratio = shakeCurve.Evaluate(t);
        });
        OnComplete?.Invoke();
    }
}
