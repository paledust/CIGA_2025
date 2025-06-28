using UnityEngine;

[CreateAssetMenu(fileName = "LastWords_IMAGE", menuName = "Scriptable Objects/LastWords/LastWords_IMAGE")]
public class LastWords_IMAGE : LastWords_SO
{
    [SerializeField] private Sprite showingImg;
    public Sprite GetShowingImg()=>showingImg;
}
