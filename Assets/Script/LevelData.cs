using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Level", menuName = "Level Data")]
public class LevelData : ScriptableObject
{
    public int veganNumers;

    [Header("Bowl")]
    [Space]
    public Image[] bowlImage;
    public Sprite[] iconSprite;
    public Color[] colors;

}
