using System.Collections;
using UnityEngine;

public class Equipment_IMG : Equipment
{
    [SerializeField] private SpriteRenderer screenTex;
    [SerializeField] private Animation screenAnime;
    private int ScreenTexID = Shader.PropertyToID("_ScreenTex");
    public override void ProcessContent(LastWords_SO lastWords)
    {
        var imgData = (lastWords as LastWords_IMAGE).GetShowingImg();
        StartCoroutine(coroutineProcessImage(imgData));
    }
    public override void ClearContent()
    {
        base.ClearContent();
        screenTex.material.SetTexture(ScreenTexID, Texture2D.whiteTexture);
    }
    IEnumerator coroutineProcessImage(Texture2D imgData)
    {
        yield return new WaitForSeconds(0.5f);
        screenAnime.Play("popimage");
        yield return new WaitForSeconds(0.1f);
        screenTex.material.SetTexture(ScreenTexID, imgData);
    }
}
