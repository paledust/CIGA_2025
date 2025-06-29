using DG.Tweening;
using Febucci.UI;
using UnityEngine;

public class Equipment_TEXT : Equipment
{
    [SerializeField] private TypewriterByCharacter typeWriter;
    [SerializeField] private GameObject labelSelectGroup;
    [SerializeField] private LabelSelect[] labelSelects;
    private string[] labels;
    private int currentNum;
    private string currentDeadType;
    private DragableNotes pendingNotes;
    void OnEnable()
    {
        EventHandler.E_OnInsertLabel += OnInsertLabelHandler;
        EventHandler.E_OnChooseLabel += OnChooseLabelHandler;
    }
    void OnDisable()
    {
        EventHandler.E_OnInsertLabel -= OnInsertLabelHandler;
        EventHandler.E_OnChooseLabel -= OnChooseLabelHandler;
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
}
