using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Lofelt.NiceVibrations;

public class VegetablesSpawn : MonoBehaviour
{
    [Header("Buttons")]
    [Space]
    [SerializeField] private Button[] _buttons;

    [Header("Vegetables")]
    [Space]
    [SerializeField] private GameObject[] _vegans;

    private UI _gameUI;

    private void Awake()
    {
        _gameUI = FindObjectOfType<UI>();
        _buttons[0].onClick.AddListener(SpawnCarrot);
        _buttons[1].onClick.AddListener(SpawnTomato);
        _buttons[2].onClick.AddListener(SpawnCucumber);
    }

    private void Start()
    {
        ButtonAnim("Show");
    }

    private void SpawnCarrot()
    {
        HapticPatterns.PlayPreset(HapticPatterns.PresetType.Selection);
        ButtonAnim("TakeOut");
        Instantiate(_vegans[0], new Vector3(-1f, 2.31f, -2.6f), Quaternion.Euler(new Vector3(-12, -111, 0)));
    }

    private void SpawnTomato()
    {
        HapticPatterns.PlayPreset(HapticPatterns.PresetType.Selection);
        ButtonAnim("TakeOut");
        Instantiate(_vegans[1], new Vector3(-1f, 2.42f, -2.4f), Quaternion.Euler(new Vector3(-3.5f, -34, 86)));
    }

    private void SpawnCucumber()
    {
        HapticPatterns.PlayPreset(HapticPatterns.PresetType.Selection);
        ButtonAnim("TakeOut");
        Instantiate(_vegans[2], new Vector3(-1f, 2.4f, -2.57f), Quaternion.Euler(new Vector3(-11.52f, -110, -8.5f)));
    }

    public void ButtonAnim(string moveAnim)
    {
        switch (moveAnim)
        {
            case "Show":
                _buttons[0].transform.parent.DOLocalMoveX(-230, 0.5f).From(-500);
                _buttons[1].transform.parent.DOLocalMoveY(0, 0.5f).From(-500);
                _buttons[2].transform.parent.DOLocalMoveX(230, 0.5f).From(500);
                break;
            case "TakeOut":
                _buttons[0].transform.parent.DOLocalMoveX(-500, 0.5f);
                _buttons[1].transform.parent.DOLocalMoveY(-500, 0.5f);
                _buttons[2].transform.parent.DOLocalMoveX(500, 0.5f);
                break;
            default:
                break;
        }
    }

}
