using System.Collections;
using DG.Tweening;
using Febucci.UI;
using UnityEngine;

public class Equipment_TEXT : Equipment
{
    [SerializeField] private TypewriterByCharacter typeWriter;
    [SerializeField] private GameObject labelSelectGroup;
    [SerializeField] private LabelSelect[] labelSelects;
    [Header("crack")]
    [SerializeField] private Animation crackAnimation;
    [SerializeField] private GameObject crackMask;
    private string[] labels;
    private int currentNum;
    private string currentDeadType;
    private bool crackOpened = false;
    private DragableNotes pendingNotes;
    void OnEnable()
    {
        EventHandler.E_OnInsertLabel += OnInsertLabelHandler;
        EventHandler.E_OnChooseLabel += OnChooseLabelHandler;
        EventHandler.E_AfterReadLabel += OnShowLabelHandler;
    }
    void OnDisable()
    {
        EventHandler.E_OnInsertLabel -= OnInsertLabelHandler;
        EventHandler.E_OnChooseLabel -= OnChooseLabelHandler;
        EventHandler.E_AfterReadLabel -= OnShowLabelHandler;
    }
    void OnShowLabelHandler()
    {
        if (crackOpened)
        {
            crackOpened = false;
            crackAnimation.Play("crack_close");
            crackMask.SetActive(false);
        }
    }
    void OnInsertLabelHandler(DragableNotes notes)
    {
        ClearContent();
        labelSelectGroup.SetActive(true);
        int totalcount = Mathf.Min(labelSelects.Length, labels.Length);
        for (int i = 0; i < totalcount; i++)
        {
            labelSelects[i].ShowLabel(labels[i]);
        }
        pendingNotes = notes;
    }
    void OnChooseLabelHandler(string label)
    {
        for (int i = 0; i < labelSelects.Length; i++)
        {
            labelSelects[i].ClearLabel();
        }
        pendingNotes.transform.DOMove(pendingNotes.transform.position + Vector3.up * 0.75f, 0.4f).SetEase(Ease.OutBack);
        pendingNotes.GiveLabelDetail(label, currentDeadType, currentNum);
        pendingNotes.EnableHitbox();
    }
    public override void ProcessContent(LastWords_SO lastWords)
    {
        var words = lastWords as LastWords_TEXT;
        typeWriter.ShowText(words.GetShowingText());
        labels = words.GetLabels();
        StartCoroutine(coroutineOpenCrack());
    }
    public void RegisterLabelDetail(int num, string deadType)
    {
        currentDeadType = deadType;
        currentNum = num;
    }
    public override void ClearContent()
    {
        typeWriter.TextAnimator.SetText(string.Empty);
    }
    IEnumerator coroutineOpenCrack()
    {
        yield return new WaitForSeconds(3f);
        crackOpened = true;
        crackAnimation.Play("crack_open");
        crackMask.gameObject.SetActive(true);
    }
}
