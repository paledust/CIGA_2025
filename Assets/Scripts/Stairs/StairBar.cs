using UnityEngine;

public class StairBar : MonoBehaviour
{
    private Vector2 scale = new Vector2(0.95f, 0.3f);
    private Vector2 range = new Vector2(-4, 3);
    private Vector3 initScale;
    void Start()
    {
        initScale = transform.localScale;
    }
    void Update()
    {
        float localPos = transform.localPosition.y;
        float ratio = (localPos - range.x) / (range.y - range.x);
        initScale.x = Mathf.Lerp(scale.x, scale.y, ratio);
        transform.localScale = initScale;
    }
}