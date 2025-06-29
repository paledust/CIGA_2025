using System.Collections;
using SimpleAudioSystem;
using TMPro;
using UnityEngine;

public class LabelSelect : BasicInteractable
{
    [SerializeField] private SpriteRenderer m_bk;
    [SerializeField] private TextMeshPro text;
    [SerializeField] private Animation blink;
    [SerializeField] private string selectClip = "group_key";
    private string label;
    public void ShowLabel(string label)
    {
        this.label = label;
        text.text = label;
        gameObject.SetActive(true);
        EnableHitbox();
    }
    public void ClearLabel()
    {
        this.label = string.Empty;
        text.text = string.Empty;
        gameObject.SetActive(false);
        DisableHitbox();
    }
    public override void OnHover()
    {
        base.OnHover();
        AudioManager.Instance.PlaySoundEffect(selectClip, 0.5f);
        m_bk.color = Color.white;
        text.color = Color.black;
    }
    public override void OnExitHover()
    {
        base.OnExitHover();
        m_bk.color = Color.clear;
        text.color = Color.white;
    }
    public override void OnClick(PlayerController playerController)
    {
        DisableHitbox();
        AudioManager.Instance.PlaySoundEffect(selectClip, 0.5f);
        StartCoroutine(coroutineOnChooseLabel());
    }
    IEnumerator coroutineOnChooseLabel()
    {
        blink.Play();
        yield return new WaitForSeconds(blink.clip.length+0.2f);
        EventHandler.Call_OnChooseLabel(label);
    }
}
