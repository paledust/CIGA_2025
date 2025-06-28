using UnityEngine;

public class Equipment_IMG : Equipment
{
    [SerializeField] private SpriteRenderer screenTex;
    private int ScreenTexID = Shader.PropertyToID("_ScreenTex");
    public override void ProcessContent(LastWords_SO lastWords)
    {
        var imgData = (lastWords as LastWords_IMAGE).GetShowingImg();
        screenTex.material.SetTexture(ScreenTexID, imgData);
    }
    public override void ClearContent()
    {
        base.ClearContent();
        screenTex.material.SetTexture(ScreenTexID, Texture2D.whiteTexture);
    }
}
