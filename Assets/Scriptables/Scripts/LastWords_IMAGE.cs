using UnityEngine;

[CreateAssetMenu(fileName = "LastWords_IMAGE", menuName = "Scriptable Objects/LastWords/LastWords_IMAGE")]
public class LastWords_IMAGE : LastWords_SO
{
    [SerializeField] private Texture2D showingImg;
    public Texture2D GetShowingImg()=>showingImg;
}
