using TMPro;
using UnityEngine;

public class LabelDetailController : MonoBehaviour
{
    [SerializeField] private GameObject detailLabel;
    [SerializeField] private TextMeshPro tmpIndex;
    [SerializeField] private TextMeshPro tmpType;
    [SerializeField] private TextMeshPro tmpLabel;
    void Awake()
    {
        EventHandler.E_OnShowLabel += ShowDetailedLabel;
    }
    void OnDestroy()
    {
        EventHandler.E_OnShowLabel -= ShowDetailedLabel;
    }
    public void ShowDetailedLabel(LabelDetailData detailData)
    {
        string numText = (detailData.num + 1).ToString();
        char[] codeText = new char[3] { '0', '0', '0' };
        for (int i = 0; i < numText.Length; i++)
        {
            codeText[2 - i] = numText[i];
        }

        tmpIndex.text = new string(codeText);
        tmpLabel.text = detailData.label;
        tmpType.text = detailData.deadsType;
        detailLabel.SetActive(true);
    }
    public void HideDetailLabel()
    {
        detailLabel.SetActive(false);
    }
}

public struct LabelDetailData
{
    public int num;
    public string deadsType;
    public string label;
}
