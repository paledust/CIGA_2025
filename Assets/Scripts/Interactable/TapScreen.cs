using UnityEngine;

public class TapScreen : BasicInteractable
{
    [SerializeField] private Animation shakeAnime;
    [SerializeField] private Animation glitchAnime;
    [SerializeField] private float glitchCycle = 10;
    [SerializeField] private float glitchChance = 0.3f;
    [SerializeField] private float repairChance = 0.5f;
    private bool isGlitching = false;
    private float glitchTimer = 0;
    private static readonly string[] shakeClips = new string[3] { "shakeScreen_1", "shakeScreen_2", "shakeScreen_3" };
    private static readonly string[] glitchClips = new string[3] { "glitch_1", "glitch_2", "glitch_3" };
    private static readonly string repairClip = "repair";
    public override void OnClick(PlayerController playerController)
    {
        shakeAnime.Play(shakeClips[Random.Range(0, shakeClips.Length)]);
        if (Random.value <= repairChance)
        {
            isGlitching = false;
            glitchAnime.Play(repairClip);
        }
    }
    void Update()
    {
        if (!isGlitching)
        {
            glitchTimer += Time.deltaTime;
            if (glitchTimer > glitchCycle)
            {
                glitchTimer = 0;
                if (Random.value <= glitchChance)
                {
                    isGlitching = true;
                    glitchAnime.Play(glitchClips[Random.Range(0, glitchClips.Length)]);
                }
            }
        }
    }
}
