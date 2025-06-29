using DG.Tweening;
using SimpleAudioSystem;
using UnityEngine;

public class Stairs : MonoBehaviour
{
    [SerializeField] private Transform[] stairBars;
    [SerializeField] private float speed;
    [SerializeField] private string loopClip;
    [SerializeField] private string stopClip;
    [SerializeField] private string startClip;
    [SerializeField] private AudioSource loopAudio;
    private float intersection = 1.5f;
    void Start()
    {
        for (int i = 0; i < stairBars.Length; i++)
        {
            stairBars[i].position = Vector3.down * 4 + Vector3.up * intersection * i;
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < stairBars.Length; i++)
        {
            stairBars[i].position += Vector3.up * speed * Time.deltaTime;
            if (stairBars[i].position.y > 3)
            {
                Vector3 pos = stairBars[i].position;
                int next = i + 1;
                if (next >= stairBars.Length)
                {
                    next = 0;
                }
                pos = stairBars[next].position + Vector3.down * intersection;
                stairBars[i].position = pos;
            }
        }
    }
    public void SpeedUp(float targetSpeed, float duration)
    {
        AudioManager.Instance.PlaySoundEffect(stopClip, 0.25f);
        AudioManager.Instance.PlaySoundEffectLoop(loopAudio, loopClip, 0.25f, 0.5f);
        DOTween.To(() => speed, x => speed = x, targetSpeed, duration).SetEase(Ease.InQuad);
    }
    public void Stop(float duration)
    {
        AudioManager.Instance.PlaySoundEffect(startClip, 0.25f);
        AudioManager.Instance.PlaySoundEffectLoop(loopAudio, loopClip, 0, 0.5f);
        DOTween.To(()=>speed, x => speed = x, 0, duration).SetEase(Ease.OutQuad);
    }
}
