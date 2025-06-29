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
        tmpIndex.text = (detailData.num+1).ToString();
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
