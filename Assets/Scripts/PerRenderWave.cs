using UnityEngine;

public class PerRenderWave : PerRendererBehavior
{
    [Range(0, 1)] public float amplitudeControl;
    private int AmplitudeControlID = Shader.PropertyToID("_AmpControl");
    protected override void UpdateProperties()
    {
        base.UpdateProperties();
        mpb.SetFloat(AmplitudeControlID, amplitudeControl);
    }
}
