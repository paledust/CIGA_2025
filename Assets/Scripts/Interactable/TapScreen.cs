using UnityEngine;

public class TapScreen : BasicInteractable
{
    [SerializeField] private Animation shakeAnime;
    private static readonly string[] shakeClips = new string[3] { "shakeScreen_1", "shakeScreen_2", "shakeScreen_3" };
    public override void OnClick(PlayerController playerController)
    {
        shakeAnime.Play(shakeClips[Random.Range(0, shakeClips.Length)]);
    }
}
