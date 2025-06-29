using SimpleAudioSystem;
using UnityEngine;
using UnityEngine.UI;

public class Intro : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private string clickClip;
    public void EnableButtone()
    {
        button.interactable = true;
    }
    public void EnterGame()
    {
        button.interactable = false;
        AudioManager.Instance.PlaySoundEffect(clickClip, 1);
        GameManager.Instance.SwitchingScene("Main");
    }
}
