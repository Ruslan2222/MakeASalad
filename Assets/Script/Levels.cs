using UnityEngine;
using UnityEngine.UI;

public class Levels : MonoBehaviour
{
    [SerializeField] private LevelData[] _levelDatas;
    [Space]
    [SerializeField] private GameObject _bowlStart;
    [SerializeField] private GameObject _bowlGame;

    [Header("Vegan Icon")]
    [Space]
    [SerializeField] private Sprite[] _veganIcons;
    [SerializeField] private Image _goalNow;

    [Header("Colors")]
    [Space]
    [SerializeField] private Color[] _colors;
    [SerializeField] private Color _colorFill;

    private UI _gameUI;
    private LevelData _currentLevelData;

    public Image[] _gameBowl;

    private int _chips;
    private int _level;

    private void Awake()
    {
        _gameUI = FindObjectOfType<UI>();
        GetData();
    }

    private void GetData()
    {
        if (PlayerPrefs.HasKey("level"))
        {
            _level = PlayerPrefs.GetInt("level");
        }
        else
        {
            _level = 0;
            PlayerPrefs.SetInt("level", _level);
        }

        if (_level > _levelDatas.Length - 1)
        {
            _level = GetRandomLevel();
        }

        _currentLevelData = _levelDatas[_level];

        int bowlPick = _currentLevelData.veganNumers;
        _gameBowl = new Image[bowlPick];
        Color[] color = _currentLevelData.colors;
        Sprite[] sprites = _currentLevelData.iconSprite;

        SpawnLevel(bowlPick, color, sprites);

    }

    private void SpawnLevel(int bowlPick, Color[] colors, Sprite[] icons)
    {

        for (int i = 0; i < bowlPick; i++)
        {
            Image startBowl = Instantiate(_currentLevelData.bowlImage[i], _bowlStart.transform);
            Image gameBowl = Instantiate(_currentLevelData.bowlImage[i], _bowlGame.transform);
            startBowl.color = colors[i];
            startBowl.transform.GetChild(0).GetComponent<Image>().sprite = icons[i];
            _gameBowl[i] = gameBowl;
            _gameBowl[i].color = colors[i];
            _gameBowl[i].transform.GetChild(0).GetComponent<Image>().sprite = icons[i];
        }

        _goalNow.sprite = _currentLevelData.iconSprite[0];

    }
    private int GetRandomLevel()
    {
        int index = Random.Range(0, _levelDatas.Length);

        int lastLevel = PlayerPrefs.GetInt("level");
        if (index == lastLevel)
        {
            return GetRandomLevel();
        }
        else
        {
            return index;
        }
    }

    public void CheckGoal(string nameVegan)
    {
        
        if (_currentLevelData.iconSprite[_chips].name == nameVegan)
        {
            _chips += 1;

            if (_currentLevelData.bowlImage.Length > _chips)
            {
                _goalNow.sprite = _currentLevelData.iconSprite[_chips];
                _gameBowl[_chips - 1].color = _colorFill;
            }

        }

        if (_chips == _currentLevelData.veganNumers)
        {
            StartCoroutine(_gameUI.UIWinGame());
            PlayerPrefs.SetInt("level", _level += 1);
        }

    }

}
