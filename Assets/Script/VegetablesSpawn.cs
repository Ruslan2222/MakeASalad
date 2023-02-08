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

    private Vector3 spawnPoint;

    private void Awake()
    {
        _buttons[0].onClick.AddListener(SpawnCarrot);
        _buttons[1].onClick.AddListener(SpawnTomato);
        _buttons[2].onClick.AddListener(SpawnCucumber);
        spawnPoint = new Vector3(-0.338f, 2.4f, -2.89f);
    }

    private void Start()
    {
        ButtonAnim("Show");
    }

    private void SpawnCarrot()
    {
        HapticPatterns.PlayPreset(HapticPatterns.PresetType.Selection);
        ButtonAnim("TakeOut");
        GameObject vegan = Instantiate(_vegans[0], spawnPoint, Quaternion.Euler(new Vector3(-12, -120, 0)));
        vegan.name = _vegans[0].name;
    }

    private void SpawnTomato()
    {
        HapticPatterns.PlayPreset(HapticPatterns.PresetType.Selection);
        ButtonAnim("TakeOut");
        GameObject vegan = Instantiate(_vegans[1], spawnPoint, Quaternion.Euler(new Vector3(-3.5f, -47, 86)));
        vegan.name = _vegans[1].name;
    }

    private void SpawnCucumber()
    {
        HapticPatterns.PlayPreset(HapticPatterns.PresetType.Selection);
        ButtonAnim("TakeOut");
        GameObject vegan = Instantiate(_vegans[2], spawnPoint, Quaternion.Euler(new Vector3(-11.52f, -110, -8.5f)));
        vegan.name = _vegans[2].name;
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
